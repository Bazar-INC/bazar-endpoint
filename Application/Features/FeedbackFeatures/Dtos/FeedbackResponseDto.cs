
using Application.Features.AccountFeatures.Dtos;
using Application.Features.ProductFeatures.Dtos;

namespace Application.Features.FeedbackFeatures.Dtos;

public class FeedbackResponseDto
{
    public Guid Id { get; set; }
    public string? CreatedAt { get; set; }
    public string? Text { get; set; }
    public int Stars { get; set; }
    public bool IsEdited { get; set; }
    public UserDto? Owner { get; set; }
    public ProductDto? Product { get; set; }
    public ICollection<FeedbackAnswerResponseDto> Answers { get; set; } = new List<FeedbackAnswerResponseDto>();
}
