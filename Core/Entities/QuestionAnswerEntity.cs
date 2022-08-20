
using Core.Entities.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

[Table("UsrQuestionAnswers")]
public class QuestionAnswerEntity : BaseEntity
{
    // properties
    public string? Text { get; set; }
    public bool IsEdited { get; set; }

    // navigation properties
    public virtual UserEntity? Owner { get; set; }
    public virtual QuestionEntity? Question { get; set; }
}
