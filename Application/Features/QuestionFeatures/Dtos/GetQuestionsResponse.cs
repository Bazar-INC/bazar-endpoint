
namespace Application.Features.QuestionFeatures.Dtos;

public record GetQuestionsResponse
{
    public int TotalPages { get; set; }
    public ICollection<QuestionResponseDto>? Questions { get; set; }
}
