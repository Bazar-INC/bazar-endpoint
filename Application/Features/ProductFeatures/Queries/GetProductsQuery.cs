
using Application.Features.ProductFeatures.Dtos;
using AutoMapper;
using Core.Entities;
using Infrastructure.UnitOfWork.Abstract;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Application.Features.ProductFeatures.Queries;

public record GetProductsQuery : IRequest<ProductsResponseDto>
{
    public int? Page { get; set; }
    public int? PerPage { get; set; }
    public string? Category { get; set; }
    public string? FilterString { get; set; }
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
        var products = _unitOfWork.Products.Get().Include(p => p.Category).Include(p => p.FilterValues).ThenInclude(f => f.FilterName).AsQueryable();

        products = products.Where(p => p.Category!.Name == request.Category);

        if (!products.Any())
        {
            return Task.FromResult(new ProductsResponseDto()
            {
                Products = new List<ProductDto>(),
                TotalPages = 0
            });
        }

        if (!string.IsNullOrEmpty(request.FilterString))
        {
            products = Filter(products, request.FilterString);

            if (!products.Any())
            {
                return Task.FromResult(new ProductsResponseDto()
                {
                    Products = new List<ProductDto>(),
                    TotalPages = 0
                });
            }
        }

        var perPage = request.PerPage ?? AppSettings.Constants.DefaultPerPage;
        var page = request.Page ?? AppSettings.Constants.DefaultPage;

        products = Paginate(products, perPage, page, out int totalPages);

        var filters = new List<FilterValueEntity>();

        foreach (var item in products.Select(p => p.FilterValues))
        {
            filters.AddRange(item);
        }

        return Task.FromResult(new ProductsResponseDto()
        {
            Products = _mapper.Map<ICollection<ProductDto>>(products),
            TotalPages = totalPages
        });
    }

    private IQueryable<ProductEntity> Filter(IQueryable<ProductEntity> products, string? filterString)
    {
        var filters = filterString!.Split("/");

        var filterTuple = new List<(FilterNameEntity, ICollection<FilterValueEntity>)>();

        foreach (var filter in filters)
        {
            var splitted = filter.Split("-");
            ICollection<FilterValueEntity> filtersValues = new List<FilterValueEntity>();

            for (int i = 0; i < splitted.Length; i++)
            {
                if (i != 0)
                {
                    filtersValues.Add(new FilterValueEntity()
                    {
                        Code = splitted[i]
                    });
                }
            }

            var tuple = (new FilterNameEntity() { Code = splitted[0] }, filtersValues);

            filterTuple.Add(tuple);
        }

        var result = new List<ProductEntity>();

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
}