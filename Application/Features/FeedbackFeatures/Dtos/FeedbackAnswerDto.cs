
using Application.Features.AccountFeatures.Dtos;

namespace Application.Features.FeedbackFeatures.Dtos;

public class FeedbackAnswerDto
{
    public DateTime CreatedAt { get; set; }
    public string? Text { get; set; }
    public virtual UserDto? Owner { get; set; }
}
