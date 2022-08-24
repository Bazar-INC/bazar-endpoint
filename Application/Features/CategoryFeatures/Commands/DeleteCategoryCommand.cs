
using Infrastructure.UnitOfWork.Abstract;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.CommonExceptions;
using Shared.FileStorage.Abstract;

namespace Application.Features.CategoryFeatures.Commands;

public record DeleteCategoryCommand(Guid CategoryId) : IRequest;

public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorageService _fileStorageService;

    public DeleteCategoryHandler(IUnitOfWork unitOfWork, IFileStorageService fileStorageService)
    {
        _unitOfWork = unitOfWork;
        _fileStorageService = fileStorageService;
    }

    public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var categoryId = request.CategoryId;
        var category = await _unitOfWork.Categories.Get(c => c.Id == categoryId, includeProperties: "Children,Products,FilterNames").FirstOrDefaultAsync();

        if(category == null)
        {
            throw new BadRequestRestException($"Category with {categoryId} id wasn`t found");
        }

        if(category.Children.Any())
        {
            throw new BadRequestRestException($"It`s impossible to delete the category because it has {category.Children.Count} children");
        }

        if (category.FilterNames.Any())
        {
            throw new BadRequestRestException($"It`s impossible to delete the category because it has {category.FilterNames.Count} filer names");
        }

        if (category.Products.Any())
        {
            throw new BadRequestRestException($"It`s impossible to delete the category because it has {category.Products.Count} products");
        }

        if(!string.IsNullOrEmpty(category.Icon))
        {
            _fileStorageService.DeleteFile(category.Icon);
        }

        if (!string.IsNullOrEmpty(category.Image))
        {
            _fileStorageService.DeleteFile(category.Image);
        }

        _unitOfWork.Categories.Delete(category);
        await _unitOfWork.SaveChangesAsync();

        return await Unit.Task;
    }
}