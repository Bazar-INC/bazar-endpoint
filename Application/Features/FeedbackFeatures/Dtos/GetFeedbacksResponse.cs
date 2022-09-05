
namespace Application.Features.FeedbackFeatures.Dtos;

public record GetFeedbacksResponse
{
    public int TotalPages { get; set; }
    public ICollection<FeedbackResponseDto> Feedbacks { get; set; } = new List<FeedbackResponseDto>();
}
