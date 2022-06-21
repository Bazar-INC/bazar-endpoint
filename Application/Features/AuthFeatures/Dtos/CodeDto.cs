namespace Application.Features.AuthFeatures.Dtos;

public record CodeDto
{
    public DateTime CreatedAt { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Code { get; set; }
}