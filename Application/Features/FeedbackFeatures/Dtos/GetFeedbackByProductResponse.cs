
namespace Application.Features.FeedbackFeatures.Dtos;
public record GetFeedbackByProductResponse
{
    public ICollection<FeedbackDto> Feedbacks { get; set; } = new List<FeedbackDto>();
}
