
using Application.Features.ProductFeatures.Dtos;
using AutoMapper;
using Infrastructure.UnitOfWork.Abstract;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.CommonExceptions;

namespace Application.Features.ProductFeatures.Queries;

public record GetProductQuery (Guid Id) : IRequest<ProductDto>;

public class GetProductCommand : IRequestHandler<GetProductQuery, ProductDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetProductCommand(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ProductDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Products.Get(p => p.Id == request.Id, includeProperties: "Images").FirstOrDefaultAsync();

        if(product == null)
        {
            throw new BadRequestRestException("Product wasn`t found");
        }

        return _mapper.Map<ProductDto>(product);
    }
}
