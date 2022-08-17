using Application.Features.AccountFeatures.Dtos;

namespace Application.Features.AuthFeatures.Dtos;

public record ConfirmResponseDto
{
    public string? Token { get; set; }
    public UserDto? Profile { get; set; }
}