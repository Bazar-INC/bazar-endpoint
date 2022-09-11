
using Application.Features.CategoryFeatures.Dtos;
using Application.Features.ProductFeatures.Dtos;

namespace Application.Features.FiltersFeatures.Dtos;

public class FilterDto
{
    public string? Name { get; set; }
    public string? Code { get; set; }

    public ICollection<FilterValueDto> FilterValues { get; set; } = new List<FilterValueDto>();
    public CategoryDto? Category { get; set; }
}
