namespace Application.Features.ProductFeatures.Dtos;

public record FilterNameDto
{
    public string? Name { get; set; }
    public string? Code { get; set; }

    public ICollection<FilterValueDto> Options { get; set; } = new List<FilterValueDto>();
}
