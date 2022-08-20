
using Application.Features.AccountFeatures.Dtos;

namespace Application.Features.QuestionFeatures.Dtos;

public class QuestionAnswerResponseDto
{
    public string? CreatedAt { get; set; }
    public string? Text { get; set; }
    public virtual UserDto? Owner { get; set; }
}