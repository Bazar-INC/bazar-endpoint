
using Application.Features.ProductFeatures.Dtos;

namespace Application.Features.CategoryFeatures.Dtos;

public class CategoryDto
{
    public Guid Id { get; set; }

    public string? Name { get; set; }
    public string? Image { get; set; }
    public Guid? ParentId { get; set; }

    public ICollection<ProductDto>? Products { get; set; }
    public ICollection<CategoryDto>? Children { get; set; }
    public CategoryDto? Parent { get; set; }
}
