
using Infrastructure.UnitOfWork.Abstract;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.CommonExceptions;
using Shared.FileStorage.Abstract;

namespace Application.Features.ProductFeatures.Commands;

public record DeleteProductCommand(Guid ProductId) : IRequest;

public class DeleteProductHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorageService _fileStorageService;

    public DeleteProductHandler(IUnitOfWork unitOfWork, IFileStorageService fileStorageService)
    {
        _unitOfWork = unitOfWork;
        _fileStorageService = fileStorageService;
    }

    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var productId = request.ProductId;
        var product = await _unitOfWork.Products.Get(p => p.Id == productId, includeProperties: "Images").FirstOrDefaultAsync();

        if(product == null)
        {
            throw new BadRequestRestException($"Product with id {productId} wasn`t found");
        }

        if(product.Images.Any())
        {
            foreach (var image in product.Images)
            {
                _fileStorageService.DeleteFile(image.Path!);
            }
        }

        _unitOfWork.Products.Delete(product);
        await _unitOfWork.SaveChangesAsync();
        return await Unit.Task;
    }
}