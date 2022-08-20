
using Application.Features.AccountFeatures.Dtos;

namespace Application.Features.FeedbackFeatures.Dtos;

public class FeedbackResponseDto
{
    public string? CreatedAt { get; set; }
    public string? Text { get; set; }
    public int Stars { get; set; }
    public virtual UserDto? Owner { get; set; }
    public virtual ICollection<FeedbackAnswerResponseDto> Answers { get; set; } = new List<FeedbackAnswerResponseDto>();
}
