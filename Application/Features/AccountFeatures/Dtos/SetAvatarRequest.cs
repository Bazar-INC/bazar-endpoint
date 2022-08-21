
namespace Application.Features.AccountFeatures.Dtos;

public record SetAvatarRequest
{
    public string? Avatar { get; set; }
    public string? FileName { get; set; }
}
