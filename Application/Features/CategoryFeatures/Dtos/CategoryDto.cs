namespace Application.Features.CategoryFeatures.Dtos;

public class CategoryDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? Image { get; set; }
    public string? Icon { get; set; }
    public string? ParentCode { get; set; }

    public ICollection<CategoryDto>? Children { get; set; }
}
