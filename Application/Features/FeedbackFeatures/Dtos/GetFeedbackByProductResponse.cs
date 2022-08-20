
namespace Application.Features.FeedbackFeatures.Dtos;
public record GetFeedbackByProductResponse
{
    public ICollection<FeedbackResponseDto> Feedbacks { get; set; } = new List<FeedbackResponseDto>();
}
