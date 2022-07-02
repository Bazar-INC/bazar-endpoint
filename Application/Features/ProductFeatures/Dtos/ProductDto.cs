
namespace Application.Features.ProductFeatures.Dtos;

public record ProductDto
{
    public Guid Id { get; set; }

    public string? Name { get; set; }
    public decimal Price { get; set; }
}
