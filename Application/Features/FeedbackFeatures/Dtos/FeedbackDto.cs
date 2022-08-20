
using Application.Features.AccountFeatures.Dtos;

namespace Application.Features.FeedbackFeatures.Dtos;

public class FeedbackDto
{
    public string? CreatedAt { get; set; }
    public string? Text { get; set; }
    public virtual UserDto? Owner { get; set; }
    public virtual ICollection<FeedbackAnswerDto> Answers { get; set; } = new List<FeedbackAnswerDto>();
}
