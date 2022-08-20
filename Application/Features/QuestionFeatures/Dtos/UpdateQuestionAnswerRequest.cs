
namespace Application.Features.QuestionFeatures.Dtos;

public record UpdateQuestionAnswerRequest
{
    public Guid QuestionAnswerId { get; set; }
    public string? Text { get; set; }
}
