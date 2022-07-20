
using Application.Features.ProductFeatures.Dtos;
using AutoMapper;
using Core.Entities;
using Infrastructure.UnitOfWork.Abstract;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;
using System.Collections.Generic;

namespace Application.Features.ProductFeatures.Queries;

public record GetProductsQuery : IRequest<ProductsResponseDto>
{
    public int? Page { get; set; }
    public int? PerPage { get; set; }
    public string? Category { get; set; }
    public string? FilterString { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
}

public class GetProductsHandler : IRequestHandler<GetProductsQuery, ProductsResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductsHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public Task<ProductsResponseDto> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = _unitOfWork.Products.Get()
            .Include(p => p.Category)
            .Include(p => p.FilterValues)
            .ThenInclude(f => f.FilterName)
            .AsQueryable();

        var perPage = request.PerPage ?? AppSettings.Constants.DefaultPerPage;
        var page = request.Page ?? AppSettings.Constants.DefaultPage;
        int totalPages = 0;

        if (request.Category == null)
        {
            products = FilterProductsByPrice(products, request.MinPrice, request.MaxPrice);
            products = Paginate(products, perPage, page, out totalPages);

            var response = GenerateProductsResponse(products.ToList(), totalPages, GetProductsFilters(products));

            return Task.FromResult(response);
        }

        products = products.Where(p => p.Category!.Name == request.Category);

        if (!products.Any())
        {
            return Task.FromResult(GenerateProductsResponse());
        }

        if (!string.IsNullOrEmpty(request.FilterString))
        {
            products = Filter(products, request.FilterString);

            if (!products.Any())
            {
                return Task.FromResult(GenerateProductsResponse());
            }
        }

        products = FilterProductsByPrice(products, request.MinPrice, request.MaxPrice);
        products = Paginate(products, perPage, page, out totalPages);

        {
            var response = GenerateProductsResponse(products.ToList(), totalPages, GetProductsFilters(products));

            return Task.FromResult(response);
        }
    }

    #region Private

    private ProductsResponseDto GenerateProductsResponse(
        ICollection<ProductEntity>? products = null!,
        int totalPages = 0,
        ICollection<FilterNameDto> filters = null!)
    {
        if(products == null || !products.Any())
        {
            return new ProductsResponseDto();
        }

        var response = new ProductsResponseDto()
        {
            Products = _mapper.Map<ICollection<ProductDto>>(products)
        };

        var prices = GetMinAndMaxPrices(products);

        response.TotalPages = totalPages;

        response.MinPrice = prices.Item1;
        response.MaxPrice = prices.Item2;

        if(filters != null)
        {
            response.Filters = filters;
        }

        return response;
    }
    
    private (decimal, decimal) GetMinAndMaxPrices(ICollection<ProductEntity> products)
    {
        var minPrice = products.Min(p => p.Price);
        var maxPrice = products.Max(p => p.Price);

        return (minPrice, maxPrice);
    }

    private IQueryable<ProductEntity> FilterProductsByPrice(IQueryable<ProductEntity> products, decimal? minPrice, decimal? maxPrice)
    {
        if(minPrice != null)
        {
            products = products.Where(p => p.Price >= minPrice);
        }

        if (maxPrice != null)
        {
            products = products.Where(p => p.Price <= maxPrice);
        }

        return products;
    }

    private IQueryable<ProductEntity> Filter(IQueryable<ProductEntity> products, string? filterString)
    {
        var filterTuple = SplitFilterString(filterString);

        var result = new List<ProductEntity>();

        // getting all existing filter names codes
        var filterNamesCodes = _unitOfWork.FilterNames.Get().Select(f => f.Code);

        // and all from request
        var filterNamesCodesFromRequest = filterTuple.Select(f => f.Item1.Code);

        // check if there is no non-existing filterCode in filterCodes
        //
        // e.g. request can contain next filterString: ram-4_gb/[bob]-64_gb
        // there is no filter name such as [bob] so we have to return empty result list
        bool hasAll = filterNamesCodesFromRequest.All(code => filterNamesCodes.Contains(code));

        if (!hasAll)
        {
            return result.AsQueryable();
        }

        // variable to check wheather product contain all filters from filterString
        bool isProductContainAllFilters = false;

        foreach (var product in products)
        {
            isProductContainAllFilters = false;
            foreach (var tuple in filterTuple)
            {
                foreach (var filter in product.FilterValues)
                {
                    if (tuple.Item1.Code == filter.FilterName!.Code)
                    {
                        if (tuple.Item2.FirstOrDefault(f => f.Code == filter.Code) != null)
                        {
                            isProductContainAllFilters = true;
                            continue;
                        }
                        else
                        {
                            isProductContainAllFilters = false;
                            break;
                        }
                    }
                }
                if (!isProductContainAllFilters) break;
            }
            if (isProductContainAllFilters)
            {
                result.Add(product);
            }
        }

        return result.AsQueryable();
    }

    private IQueryable<ProductEntity> Paginate(IQueryable<ProductEntity> products, int perPage, int page, out int totalPages)
    {
        var count = products.Count();

        products = products.OrderBy(p => p.Name);

        products = products.Skip((page - 1) * perPage).Take(perPage);

        totalPages = count / perPage;

        if (totalPages % perPage > 0)
        {
            ++totalPages;
        }

        return products;
    }

    /// <summary>
    /// method to split filter string
    /// </summary>
    /// <param name="filterString">filter string in format: ram-4_gb-8_gb/internal_memory-64_gb</param>
    /// <returns>Tuple list where Item1 is a filter name, Item2 is a list of filter values</returns>
    private List<(FilterNameDto, ICollection<FilterValueDto>)> SplitFilterString(string? filterString)
    {
        // e.g. filterString has next format: ram-4_gb-8_gb/internal_memory-64_gb

        // split by [/] and we get list of strings grouped by filter name
        var filters = filterString!.Split("/");

        // creating tuple that will contain
        //                          the name of a filter | list of filters
        //                              {ram}               [4_gb, 8_gb]
        var filterTuple = new List<(FilterNameDto, ICollection<FilterValueDto>)>();

        foreach (var filter in filters)
        {
            // split by a [-] and we`ll get a list of items of one filter
            // splitted[0] is a filterName (ram)
            // splitted[1] is a filterValue (4_gb)
            // splitted[2] is a filterValue (8_gb)
            // and so on ...
            var splitted = filter.Split("-");

            // this is a collection that is going to contain list of filter values (4_gb, 8_gb)
            ICollection<FilterValueDto> filtersValues = new List<FilterValueDto>();

            // iterating our items
            for (int i = 0; i < splitted.Length; i++)
            {
                // if i == 0 it means that it`s a filter name
                // otherwise it`s a filter value
                if (i != 0)
                {
                    filtersValues.Add(new FilterValueDto()
                    {
                        Code = splitted[i]
                    });
                }
            }

            // creating a tuple with filter name and filter values
            var tuple = (new FilterNameDto() { Code = splitted[0] }, filtersValues);

            // adding it to list
            filterTuple.Add(tuple);
        }

        return filterTuple;
    }

    private ICollection<FilterNameDto> GetProductsFilters(IQueryable<ProductEntity> products)
    {
        var filters = new List<FilterNameDto>();

        var filterValues = products.Select(p => p.FilterValues);

        var allFilters = new List<FilterValueEntity>();

        foreach (var f in filterValues)
        {
            allFilters.AddRange(f);
        }

        allFilters = allFilters.Distinct().ToList();

        var grouped = allFilters.GroupBy(f => f.FilterName);

        foreach (var group in grouped)
        {
            var filterName = group.Select(g => g.FilterName).FirstOrDefault();
            filters.Add(new FilterNameDto()
            {
                Name = filterName!.Name,
                Code = filterName!.Code,
                Options = _mapper.Map<ICollection<FilterValueDto>>(group.Select(g => g))
            });
        }

        return filters;
    }
    #endregion
}