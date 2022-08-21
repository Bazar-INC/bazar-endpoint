
using Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.CommonExceptions;
using Shared.FileStorage.Abstract;

namespace Application.Features.AccountFeatures.Commands;

public record DeleteAvatarCommand(Guid UserId) : IRequest;

public class DeleteAvatarHandler : IRequestHandler<DeleteAvatarCommand>
{
    private readonly IFileStorageService _fileStorageService;
    private readonly UserManager<UserEntity> _userManager;

    public DeleteAvatarHandler(IFileStorageService fileStorageService, UserManager<UserEntity> userManager)
    {
        _fileStorageService = fileStorageService;
        _userManager = userManager;
    }

    public async Task<Unit> Handle(DeleteAvatarCommand request, CancellationToken cancellationToken)
    {
        var userId = request.UserId;
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user == null)
        {
            throw new BadRequestRestException($"User with id {userId} wasn`t found");
        }

        var image = user.Image;

        if (string.IsNullOrEmpty(image))
        {
            throw new BadRequestRestException("Image is empty. Nothing to delete");
        }

        _fileStorageService.DeleteFile(user.Image!);

        user.Image = null;
        await _userManager.UpdateAsync(user);

        return await Unit.Task;
    }
}