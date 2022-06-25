using Application.Features.AuthFeatures.Dtos;
using Application.Features.AuthFeatures.Services;
using Core.Entities;
using FluentValidation;
using Infrastructure.UnitOfWork.Abstract;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared;
using Shared.CommonExceptions;
using Shared.Validators;

namespace Application.Features.AuthFeatures.Commands;

public record ConfirmCodeCommand(string Code, string Phone) : IRequest<ConfirmResponseDto>;

public class ConfirmCodeHandler : IRequestHandler<ConfirmCodeCommand, ConfirmResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<UserEntity> _userManager;
    private readonly JwtService _jwtService;

    public ConfirmCodeHandler(IUnitOfWork unitOfWork, UserManager<UserEntity> userManager, JwtService jwtService)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _jwtService = jwtService;
    }

    public async Task<ConfirmResponseDto> Handle(ConfirmCodeCommand request, CancellationToken cancellationToken)
    {
        var phoneNumber = PhoneValidator.RemoveWhiteSpaces(request.Phone);

        var code = _unitOfWork.Codes.Get()
                                    .Where(p => p.PhoneNumber == phoneNumber)
                                    .OrderByDescending(c => c.CreatedAt)
                                    .FirstOrDefault();

        if(code is null)
        {
            throw new BadRequestRestException("Wrong phone number");
        }

        if(code.Code != request.Code)
        {
            throw new BadRequestRestException("Wrong code");
        }

        if (DateTime.UtcNow.Subtract(code.CreatedAt) > AppSettings.Sms.CodeLifetime)
        {
            throw new BadRequestRestException("Code is expired");
        }

        var user = _userManager.Users.FirstOrDefault(u => u.PhoneNumber == phoneNumber);

        if(user is null)
        {
            user = new UserEntity()
            {
                PhoneNumber = phoneNumber,
                PhoneNumberConfirmed = true,
                UserName = phoneNumber
            };

            await _userManager.CreateAsync(user);

            await _userManager.AddToRoleAsync(user, AppSettings.Roles.Customer);
        }

        return new ConfirmResponseDto()
        {
            Token = _jwtService.GenerateToken(
            user.Id.ToString(),
            string.Join(", ", await _userManager.GetRolesAsync(user)),
            AppSettings.JwtTokenLifetimes.DefaultExpirationTime)
        };
    }
}

public class ConfirmCodeCommandValidator : AbstractValidator<ConfirmCodeCommand>
{
    public ConfirmCodeCommandValidator()
    {
        RuleFor(x => x.Phone).NotNull().Length(AppSettings.Sms.PhoneLength)
                             .Must((a, b) => a.Phone.All(char.IsDigit));

        RuleFor(x => x.Code).NotNull().Length(AppSettings.Sms.CodeLength)
                             .Must((a, b) => a.Code.All(char.IsDigit));
    }
}