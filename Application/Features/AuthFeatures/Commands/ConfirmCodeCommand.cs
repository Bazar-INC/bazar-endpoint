using Application.Features.AuthFeatures.Dtos;
using Application.Features.AuthFeatures.Services;
using AutoMapper;
using Core.Entities;
using Infrastructure.UnitOfWork.Abstract;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared;

namespace Application.Features.AuthFeatures.Commands;

public record ConfirmCodeCommand(string Code, string PhoneNumber) : IRequest<ConfirmResponseDto>;

public class ConfirmCodeHandler : IRequestHandler<ConfirmCodeCommand, ConfirmResponseDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<UserEntity> _userManager;
    private readonly JwtService _jwtService;

    public ConfirmCodeHandler(IMapper mapper, IUnitOfWork unitOfWork, UserManager<UserEntity> userManager, JwtService jwtService)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _jwtService = jwtService;
    }

    public async Task<ConfirmResponseDto> Handle(ConfirmCodeCommand request, CancellationToken cancellationToken)
    {
        var phoneNumber = request.PhoneNumber;
        
        var code = _unitOfWork.Codes.Get()
                                    .Where(p => p.PhoneNumber == phoneNumber)
                                    .OrderByDescending(c => c.CreatedAt)
                                    .FirstOrDefault();

        if(code is null)
        {
            // wrong phone number
            // TODO: throw
            return null;
        }

        if(DateTime.UtcNow.Subtract(code.CreatedAt) > AppSettings.Sms.CodeLifetime)
        {
            // code is expired
            // TODO: throw
            return null;
        }

        var user = _userManager.Users.FirstOrDefault(u => u.PhoneNumber == phoneNumber);

        if(user is null)
        {
            user = new UserEntity()
            {
                PhoneNumber = phoneNumber,
                PhoneNumberConfirmed = true,
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