
using Infrastructure.UnitOfWork.Abstract;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.CommonExceptions;
using Shared.FileStorage.Abstract;

namespace Application.Features.ProductFeatures.Commands;

public record DeleteProductImageCommand : IRequest
{
    [FromRoute(Name = "productId")]
    public Guid ProductId { get; set; }
    [FromRoute(Name = "fileName")]
    public string? FileName { get; set; }
}

public class DeleteProductImageHandler : IRequestHandler<DeleteProductImageCommand>
{
    private readonly IFileStorageService _fileStorageService;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductImageHandler(IFileStorageService fileStorageService, IUnitOfWork unitOfWork)
    {
        _fileStorageService = fileStorageService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteProductImageCommand request, CancellationToken cancellationToken)
    {
        var productId = request.ProductId;

        var product = await _unitOfWork.Products.Get().Include(p => p.Images).FirstOrDefaultAsync(p => p.Id == productId);

        if (product == null)
        {
            throw new BadRequestRestException($"Product with id {productId} wan`t found");
        }

        var productImage = product.Images.FirstOrDefault(i => i.Path!.Contains(request.FileName!));

        if(productImage == null)
        {
            throw new BadRequestRestException($"Image with path {request.FileName} wasn`t found");
        }

        product.Images.Remove(productImage);
        await _unitOfWork.SaveChangesAsync();

        _unitOfWork.Images.Delete(productImage);
        await _unitOfWork.SaveChangesAsync();

        _fileStorageService.DeleteFile(productImage.Path!);

        return await Unit.Task;
    }
}
