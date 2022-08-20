
namespace Application.Features.QuestionFeatures.Dtos;

public record AddQuestionAnswerRequest
{
    public Guid QuestionId { get; set; }
    public string? Text { get; set; }
}
