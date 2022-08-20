
using Core.Entities.Abstract;

namespace Core.Entities;

public class QuestionAnswerEntity : BaseEntity
{
    // properties
    public string? Text { get; set; }
    public bool IsEdited { get; set; }

    // navigation properties
    public virtual UserEntity? Owner { get; set; }
    public virtual QuestionEntity? Question { get; set; }
}
