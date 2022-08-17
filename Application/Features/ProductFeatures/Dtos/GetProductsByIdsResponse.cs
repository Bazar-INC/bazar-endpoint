
namespace Application.Features.ProductFeatures.Dtos;

public record GetProductsByIdsResponse
{
    public ICollection<ProductDto> Products { get; set; } = new List<ProductDto>();
}
