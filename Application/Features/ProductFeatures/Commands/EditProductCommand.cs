
using AutoMapper;
using Core.Entities;
using FluentValidation;
using Infrastructure.UnitOfWork.Abstract;
using MediatR;

namespace Application.Features.ProductFeatures.Commands;

public record EditProductCommand : IRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public string? Description { get; set; }

    public Guid CategoryId { get; set; }
}

public class EditProductHandler : IRequestHandler<EditProductCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public EditProductHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(EditProductCommand request, CancellationToken cancellationToken)
    {
        var productEntity = _mapper.Map<ProductEntity>(request);

        _unitOfWork.Products.Update(productEntity);
        await _unitOfWork.SaveChangesAsync();
        return await Unit.Task;
    }
}

public class EditProductCommandValidator : AbstractValidator<EditProductCommand>
{
    public EditProductCommandValidator()
    {
        RuleFor(f => f.Name).NotEmpty().NotNull();
        RuleFor(f => f.Price).GreaterThanOrEqualTo(0);
        RuleFor(f => f.Discount).GreaterThanOrEqualTo(0);
    }
}