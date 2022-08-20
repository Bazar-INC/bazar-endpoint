

namespace Application.Features.FeedbackFeatures.Dtos;

public record AddFeedbackRequest
{
    public string? Text { get; set; }
    public int Stars { get; set; }
    public Guid ProductId { get; set; }
}
