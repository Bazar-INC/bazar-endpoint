
using Application.Features.ProductFeatures.Dtos;
using AutoMapper;
using Infrastructure.UnitOfWork.Abstract;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ProductFeatures.Queries;

public record GetProductsQuery : IRequest<ProductsResponseDto>
{
    public int Page { get; set; }
    public int PerPage { get; set; }
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
        var products = _unitOfWork.Products.Get().Include(p => p.Category).AsQueryable();

        int count = products.Count();
        int perPage = request.PerPage;

        products = products.OrderBy(p => p.Name);
        products = products.Skip((request.Page - 1) * perPage).Take(perPage);

        int totalPages = count / perPage;

        if (totalPages % perPage > 0)
        {
            ++totalPages;
        }

        return Task.FromResult( new ProductsResponseDto()
        {
            Products = _mapper.Map<ICollection<ProductDto>>(products),
            TotalPages = totalPages
        });
    }
}