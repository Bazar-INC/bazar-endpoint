

namespace Application.Features.FeedbackFeatures.Dtos;

public record UpdateFeedbackRequest
{
    public Guid FeedbackId { get; set; }
    public string? Text { get; set; }
    public int Stars { get; set; }
}
