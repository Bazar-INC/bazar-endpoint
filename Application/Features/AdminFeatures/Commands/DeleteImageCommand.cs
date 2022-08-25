
using FluentValidation;
using MediatR;
using Shared.CommonExceptions;
using Shared.FileStorage.Abstract;

namespace Application.Features.AdminFeatures.Commands;

public record DeleteImageCommand(string FileName) : IRequest;

public class DeleteImageHandler : IRequestHandler<DeleteImageCommand>
{
    private readonly IFileStorageService _fileStorageService;

    public DeleteImageHandler(IFileStorageService fileStorageService)
    {
        _fileStorageService = fileStorageService;
    }

    public Task<Unit> Handle(DeleteImageCommand request, CancellationToken cancellationToken)
    {
        if(!_fileStorageService.IsFileExist(request.FileName))
        {
            throw new BadRequestRestException($"File {request.FileName} doesn`t exist");
        }

        _fileStorageService.DeleteFile(request.FileName);

        return Unit.Task;
    }
}

public class DeleteImageValidator : AbstractValidator<DeleteImageCommand>
{
    public DeleteImageValidator()
    {
        RuleFor(i => i.FileName).NotEmpty().NotNull();
    }
}