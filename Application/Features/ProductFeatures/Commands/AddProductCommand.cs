
using AutoMapper;
using Core.Entities;
using FluentValidation;
using Infrastructure.UnitOfWork.Abstract;
using MediatR;
using Shared.CommonExceptions;

namespace Application.Features.ProductFeatures.Commands;

public record AddProductCommand : IRequest
{
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }

    public Guid CategoryId { get; set; }
    public ICollection<Guid>? FiltersIds { get; set; }
}

public class AddProductHandler : IRequestHandler<AddProductCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AddProductHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var categoryId = request.CategoryId;
        var category = _unitOfWork.Categories.FindAsync(categoryId);

        if(category == null)
        {
            throw new BadRequestRestException($"Category with id {category} wasn`t found");
        }
        
        var productEntity = _mapper.Map<ProductEntity>(request);

        if (request.FiltersIds != null && request.FiltersIds.Any())
        {
            var filters = _unitOfWork.FilterValues.Get(f => request.FiltersIds.Contains(f.Id));

            if(filters.Any())
            {
                productEntity.FilterValues = filters.ToList();
            }
        }

        await _unitOfWork.Products.InsertAsync(productEntity);
        await _unitOfWork.SaveChangesAsync();

        return await Unit.Task;
    }
}

public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
{
    public AddProductCommandValidator()
    {
        RuleFor(f => f.Name).NotEmpty().NotNull();
        RuleFor(f => f.Price).GreaterThanOrEqualTo(0);
    }
}