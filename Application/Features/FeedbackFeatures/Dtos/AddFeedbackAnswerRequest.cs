
namespace Application.Features.FeedbackFeatures.Dtos;

public record AddFeedbackAnswerRequest
{
    public Guid FeedbackId { get; set; }
    public string? Text { get; set; }
}
