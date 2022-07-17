
namespace Application.Features.ProductFeatures.Dtos;

public record ProductsResponseDto
{
    public ICollection<ProductDto> Products { get; set; } = new HashSet<ProductDto>();
    public int TotalPages { get; set; }
    public ICollection<FilterNameDto> Filters { get; set; } = new HashSet<FilterNameDto>();
}
