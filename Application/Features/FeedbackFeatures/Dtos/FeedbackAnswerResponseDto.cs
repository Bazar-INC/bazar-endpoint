
using Application.Features.AccountFeatures.Dtos;

namespace Application.Features.FeedbackFeatures.Dtos;

public class FeedbackAnswerResponseDto
{
    public string? CreatedAt { get; set; }
    public string? Text { get; set; }
    public UserDto? Owner { get; set; }
}
