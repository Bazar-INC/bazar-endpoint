namespace Application.Features.CategoryFeatures.Dtos;

public record CategoriesResponseDto
{
    public ICollection<CategoryDto>? Categories { get; set; }
}
