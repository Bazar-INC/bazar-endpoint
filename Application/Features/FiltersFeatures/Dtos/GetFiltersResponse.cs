

namespace Application.Features.FiltersFeatures.Dtos;

public class GetFiltersResponse
{
    public ICollection<FilterDto> Filters { get; set; } = new List<FilterDto>();
    public int TotalPages { get; set; }
}
