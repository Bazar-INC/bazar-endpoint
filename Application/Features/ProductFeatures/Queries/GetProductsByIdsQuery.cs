
using Application.Features.ProductFeatures.Dtos;
using AutoMapper;
using Infrastructure.UnitOfWork.Abstract;
using MediatR;

namespace Application.Features.ProductFeatures.Queries;

public record GetProductsByIdsQuery(ICollection<Guid> ProductsIds) : IRequest<GetProductsByIdsResponse>;

public class GetProductsByIdsHandler : IRequestHandler<GetProductsByIdsQuery, GetProductsByIdsResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductsByIdsHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public Task<GetProductsByIdsResponse> Handle(GetProductsByIdsQuery request, CancellationToken cancellationToken)
    {
        var products = _unitOfWork.Products.Get(p => request.ProductsIds.Contains(p.Id), includeProperties: "Images");

        return Task.FromResult(new GetProductsByIdsResponse()
        {
            Products = _mapper.Map<ICollection<ProductDto>>(products)
        });
    }
}
