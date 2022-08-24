
using Application.Features.CategoryFeatures.Dtos;
using AutoMapper;
using Infrastructure.UnitOfWork.Abstract;
using MediatR;
using Shared.CommonExceptions;

namespace Application.Features.CategoryFeatures.Queries;
public record GetCategoryByIdQuery(Guid Id) : IRequest<CategoryDto>;

public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCategoryByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var categoryId = request.Id;
        var category = _unitOfWork.Categories.Get(filter: c => c.Id == categoryId, includeProperties: "Children").FirstOrDefault();

        if (category == null)
        {
            throw new BadRequestRestException($"Category with {categoryId} id wasn`t found");
        }

        return Task.FromResult(_mapper.Map<CategoryDto>(category));
    }
}