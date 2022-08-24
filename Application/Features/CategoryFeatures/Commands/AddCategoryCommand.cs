
using AutoMapper;
using Core.Entities;
using FluentValidation;
using Infrastructure.UnitOfWork.Abstract;
using MediatR;
using Shared.FileStorage.Abstract;

namespace Application.Features.CategoryFeatures.Commands;

public record AddCategoryCommand : IRequest
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? ImageName { get; set; }
    public string? Image { get; set; }
    public string? IconName { get; set; }
    public string? Icon { get; set; }
    public Guid? ParentId { get; set; }
}

public class AddCategoryHandler : IRequestHandler<AddCategoryCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IFileStorageService _fileStorageService;

    public AddCategoryHandler(IUnitOfWork unitOfWork, IMapper mapper, IFileStorageService fileStorageService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _fileStorageService = fileStorageService;
    }

    public async Task<Unit> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
    {
        var categoryEntity = _mapper.Map<CategoryEntity>(request);

        if(!string.IsNullOrEmpty(request.Image))
        {
            var actualImagePath = _fileStorageService.SaveCategoryImage(request.Image!, request.ImageName!, Guid.NewGuid());
            categoryEntity.Image = actualImagePath;
        }

        if (!string.IsNullOrEmpty(request.Icon))
        {
            var actualIconPath = _fileStorageService.SaveCategoryIcon(request.Icon!, request.IconName!, Guid.NewGuid());
            categoryEntity.Icon = actualIconPath;
        }

        await _unitOfWork.Categories.InsertAsync(categoryEntity);
        await _unitOfWork.SaveChangesAsync();

        return await Unit.Task;
    }
}

public class AddCategoryCommandValidator : AbstractValidator<AddCategoryCommand>
{
    public AddCategoryCommandValidator()
    {
        RuleFor(c => c.Code).NotEmpty().NotNull();
        RuleFor(c => c.Name).NotEmpty().NotNull();
    }
}