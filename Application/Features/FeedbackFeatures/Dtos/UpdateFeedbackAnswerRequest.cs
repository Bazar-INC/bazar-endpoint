
namespace Application.Features.FeedbackFeatures.Dtos;

public record UpdateFeedbackAnswerRequest
{
    public Guid FeedbackAnswerId { get; set; }
    public string? Text { get; set; }
}
