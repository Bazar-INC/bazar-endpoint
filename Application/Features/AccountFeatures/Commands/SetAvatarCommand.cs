
using Application.Features.AccountFeatures.Dtos;
using Core.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.CommonExceptions;
using Shared.FileStorage.Abstract;

namespace Application.Features.AccountFeatures.Commands;

public record SetAvatarCommand : SetAvatarRequest, IRequest
{
    public Guid UserId { get; set; }
}

public class SetAvatarHandler : IRequestHandler<SetAvatarCommand>
{
    private readonly IFileStorageService _fileStorageService;
    private readonly UserManager<UserEntity> _userManager;

    public SetAvatarHandler(IFileStorageService fileStorageService, UserManager<UserEntity> userManager)
    {
        _fileStorageService = fileStorageService;
        _userManager = userManager;
    }

    public async Task<Unit> Handle(SetAvatarCommand request, CancellationToken cancellationToken)
    {
        var userId = request.UserId;
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if(user == null)
        {
            throw new BadRequestRestException($"User with id {userId} wasn`t found");
        }

        if(!string.IsNullOrEmpty(user.Image))
        {
            _fileStorageService.DeleteFile(user.Image);
        }

        var actualPath = _fileStorageService.SaveUserAvatar(request.Avatar!, request.FileName!, userId);

        user.Image = actualPath;
        await _userManager.UpdateAsync(user);

        return await Unit.Task;
    }
}

public class SetAvatarCommandValidator : AbstractValidator<SetAvatarCommand>
{
    public SetAvatarCommandValidator()
    {
        RuleFor(f => f.Avatar).NotNull();
        RuleFor(f => f.UserId).NotEmpty();
    }
}