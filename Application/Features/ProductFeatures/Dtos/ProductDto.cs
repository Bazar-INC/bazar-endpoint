
namespace Application.Features.ProductFeatures.Dtos;

public record ProductDto
{
    public Guid Id { get; set; }

    public string? Name { get; set; }
    public decimal Price { get; set; }
    public string? CategoryName { get; set; }

    public ICollection<string> Images { get; set; } = new List<string>();
}
