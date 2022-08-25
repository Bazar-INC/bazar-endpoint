using Application.Features.AdminFeatures.Dtos;
using FluentValidation;
using MediatR;
using Shared.CommonExceptions;
using Shared.FileStorage.Abstract;

namespace Application.Features.AdminFeatures.Commands;

public record UploadImageCommand : IRequest<UploadImageResponse>
{
    public string? File { get; set; }
    public string? FileName { get; set; }
}

public class UploadImageHandler : IRequestHandler<UploadImageCommand, UploadImageResponse>
{
    private readonly IFileStorageService _fileStorageService;

    public UploadImageHandler(IFileStorageService fileStorageService)
    {
        _fileStorageService = fileStorageService;
    }

    public Task<UploadImageResponse> Handle(UploadImageCommand request, CancellationToken cancellationToken)
    {
        string filePath;

        if (string.IsNullOrEmpty(request.FileName) || Path.GetExtension(request.FileName) == string.Empty)
        {
            filePath = _fileStorageService.SaveFile(request.File!, request.FileName!, Guid.NewGuid());
        }
        else
        {
            if (_fileStorageService.IsFileExist(request.FileName!))
            {
                throw new BadRequestRestException($"File {request.FileName} already exists");
            }

            filePath = _fileStorageService.SaveFile(request.File!, request.FileName!);
        }

        return Task.FromResult(new UploadImageResponse()
        {
            FileName = Path.GetFileName(filePath)
        });
    }
}

public class UploadImageCommandValidator : AbstractValidator<UploadImageCommand>
{
    public UploadImageCommandValidator()
    {
        RuleFor(i => i.File).NotEmpty().NotNull();
    }
}