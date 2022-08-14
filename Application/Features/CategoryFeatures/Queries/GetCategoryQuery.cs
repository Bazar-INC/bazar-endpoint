
using Application.Features.CategoryFeatures.Dtos;
using AutoMapper;
using FluentValidation;
using Infrastructure.UnitOfWork.Abstract;
using MediatR;
using Shared.CommonExceptions;

namespace Application.Features.CategoryFeatures.Queries;
public record GetCategoryQuery(string Code) : IRequest<CategoryDto>;

public class GetCategoryHandler : IRequestHandler<GetCategoryQuery, CategoryDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public Task<CategoryDto> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        var category = _unitOfWork.Categories.Get(filter:c => c.Code == request.Code, includeProperties: "Children").FirstOrDefault();
        
        if(category == null)
        {
            throw new BadRequestRestException($"Category with {request.Code} wasn`t found");
        }

        return Task.FromResult(_mapper.Map<CategoryDto>(category));
    }
}

public class GetCategoryQueryValidator : AbstractValidator<GetCategoryQuery>
{
    public GetCategoryQueryValidator()
    {
        RuleFor(x => x.Code).NotNull();
    }
}