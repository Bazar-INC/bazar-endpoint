
namespace Application.Features.QuestionFeatures.Dtos;

public record GetQuestionsByProductResponse
{
    public ICollection<QuestionResponseDto> Questions { get; set; } = new List<QuestionResponseDto>();
}
