
namespace Application.Features.QuestionFeatures.Dtos;

public record AddQuestionRequest
{
    public string? Text { get; set; }
    public Guid ProductId { get; set; }
}
