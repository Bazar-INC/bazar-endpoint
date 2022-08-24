
using FluentValidation;
using Infrastructure.UnitOfWork.Abstract;
using MediatR;
using Shared.CommonExceptions;
using Shared.FileStorage.Abstract;


namespace Application.Features.CategoryFeatures.Commands;

public record EditCategoryCommand : IRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? ImageName { get; set; }
    public string? Image { get; set; }
    public string? IconName { get; set; }
    public string? Icon { get; set; }
    public Guid? ParentId { get; set; }
}

public class EditCategoryHandler : IRequestHandler<EditCategoryCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorageService _fileStorageService;

    public EditCategoryHandler(IUnitOfWork unitOfWork, IFileStorageService fileStorageService)
    {
        _unitOfWork = unitOfWork;
        _fileStorageService = fileStorageService;
    }

    public async Task<Unit> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
    {
        var categoryId = request.Id;
        var category = await _unitOfWork.Categories.FindAsync(categoryId);

        if (category == null)
        {
            throw new BadRequestRestException($"Category with {categoryId} id wasn`t found");
        }

        /*          Image          */

        // if category had image and client deleted image
        if (!string.IsNullOrEmpty(category.Image) && string.IsNullOrEmpty(request.Image))
        {
            _fileStorageService.DeleteFile(category.Image);
        }
        // if category image was empty and client added image
        else if (string.IsNullOrEmpty(category.Image) && !string.IsNullOrEmpty(request.Image))
        {
            var actualImagePath = _fileStorageService.SaveCategoryImage(request.Image!, request.ImageName!, Guid.NewGuid());
            category.Image = actualImagePath;
        }
        // if both of images aren`t empty
        else if (!string.IsNullOrEmpty(category.Image) && !string.IsNullOrEmpty(request.Image))
        {
            // if client changed image
            if (category.Image != request.ImageName)
            {
                _fileStorageService.DeleteFile(category.Image);
                var actualImagePath = _fileStorageService.SaveCategoryImage(request.Image!, request.ImageName!, Guid.NewGuid());
                category.Image = actualImagePath;
            }
            // else client didn`t change image
        }

        /*          Icon          */

        // if category had icon and client deleted icon
        if (!string.IsNullOrEmpty(category.Icon) && string.IsNullOrEmpty(request.Icon))
        {
            _fileStorageService.DeleteFile(category.Icon);
        }
        // if category icon was empty and client added icon
        else if (string.IsNullOrEmpty(category.Icon) && !string.IsNullOrEmpty(request.Icon))
        {
            var actualIconPath = _fileStorageService.SaveCategoryIcon(request.Icon!, request.IconName!, Guid.NewGuid());
            category.Icon = actualIconPath;
        }
        // if both of icons aren`t empty
        else if (!string.IsNullOrEmpty(category.Icon) && !string.IsNullOrEmpty(request.Icon))
        {
            // if client changed icon
            if (category.Icon != request.IconName)
            {
                _fileStorageService.DeleteFile(category.Icon);
                var actualIconPath = _fileStorageService.SaveCategoryIcon(request.Icon!, request.IconName!, Guid.NewGuid());
                category.Icon = actualIconPath;
            }
            // else client didn`t change icon
        }

        category.Name = request.Name;
        category.Code = request.Code;
        category.ParentId = request.ParentId;

        _unitOfWork.Categories.Update(category);
        await _unitOfWork.SaveChangesAsync();

        return await Unit.Task;
    }
}

public class EditCategoryCommandValidator : AbstractValidator<EditCategoryCommand>
{
    public EditCategoryCommandValidator()
    {
        RuleFor(c => c.Code).NotEmpty().NotNull();
        RuleFor(c => c.Name).NotEmpty().NotNull();
    }
}