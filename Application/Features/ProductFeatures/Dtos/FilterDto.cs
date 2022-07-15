
namespace Application.Features.ProductFeatures.Dtos;

public class FilterDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }

    public ICollection<FilterValueDto> Options { get; set; } = new HashSet<FilterValueDto>();
}
