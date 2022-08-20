
using Core.Entities.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

[Table("UsrFeedbackAnswers")]
public class FeedbackAnswerEntity : BaseEntity
{
    // properties
    public string? Text { get; set; }
    public bool IsEdited { get; set; }

    // navigation properties
    public virtual UserEntity? Owner { get; set; }
    public virtual FeedbackEntity? Feedback { get; set; }
}
