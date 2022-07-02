
namespace Application.Features.ProductFeatures.Dtos;

public record ProductsResponseDto
{
    public ICollection<ProductDto>? Products { get; set; }
    public int TotalPages { get; set; }
}
