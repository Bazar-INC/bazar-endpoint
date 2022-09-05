

using Application.Features.AccountFeatures.Dtos;
using Application.Features.ProductFeatures.Dtos;

namespace Application.Features.QuestionFeatures.Dtos;

public class QuestionResponseDto
{
    public Guid Id { get; set; }
    public string? CreatedAt { get; set; }
    public string? Text { get; set; }
    public bool IsEdited { get; set; }
    public virtual UserDto? Owner { get; set; }
    public virtual ProductDto? Product { get; set; }
    public virtual ICollection<QuestionAnswerResponseDto> Answers { get; set; } = new List<QuestionAnswerResponseDto>();
}