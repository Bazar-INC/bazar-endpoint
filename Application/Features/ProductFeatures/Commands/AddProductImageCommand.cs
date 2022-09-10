
using Core.Entities;
using FluentValidation;
using Infrastructure.UnitOfWork.Abstract;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.CommonExceptions;
using Shared.FileStorage.Abstract;

namespace Application.Features.ProductFeatures.Commands;

public record AddProductImageRequest : IRequest
{
    public string? Image { get; set; }
    public string? ImageName { get; set; }
    public int ImageOrder { get; set; }
}

public record AddProductImageCommand : AddProductImageRequest, IRequest
{
    public Guid ProductId { get; set; }
}

public class AddProductImageHandler : IRequestHandler<AddProductImageCommand>
{
    private readonly IFileStorageService _fileStorageService;
    private readonly IUnitOfWork _unitOfWork;

    public AddProductImageHandler(IFileStorageService fileStorageService, IUnitOfWork unitOfWork)
    {
        _fileStorageService = fileStorageService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(AddProductImageCommand request, CancellationToken cancellationToken)
    {
        var productId = request.ProductId;
        var product = await _unitOfWork.Products.FindAsync(productId);

        if(product == null)
        {
            throw new BadRequestRestException($"Product with id {productId} wan`t found");
        }

        var imagePath = _fileStorageService.SaveProductImage(request.Image!, request.ImageName!);

        var image = new ImageEntity()
        {
            Order = request.ImageOrder,
            Path = imagePath,
            Product = product
        };

        await _unitOfWork.Images.InsertAsync(image);
        await _unitOfWork.SaveChangesAsync();


        product.Images.Add(image);
        _unitOfWork.Products.Update(product);
        await _unitOfWork.SaveChangesAsync();

        return await Task.FromResult(await Unit.Task);
    }
}
