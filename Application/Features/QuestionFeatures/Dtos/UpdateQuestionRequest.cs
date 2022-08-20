
namespace Application.Features.QuestionFeatures.Dtos;

public record UpdateQuestionRequest
{
    public Guid QuestionId { get; set; }
    public string? Text { get; set; }
}

