
using Application.Features.CategoryFeatures.Dtos;
using AutoMapper;
using Infrastructure.UnitOfWork.Abstract;
using MediatR;

namespace Application.Features.CategoryFeatures.Queries;

public record GetCategoriesQuery : IRequest<CategoriesResponseDto>;

public class GetCategoriesHandler : IRequestHandler<GetCategoriesQuery, CategoriesResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCategoriesHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public Task<CategoriesResponseDto> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new CategoriesResponseDto
        { 
            Categories = _mapper.Map<ICollection<CategoryDto>>(_unitOfWork.Categories.Get(null!, null!, "Children"))
        });
    }
}
