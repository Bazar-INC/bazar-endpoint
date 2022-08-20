
using Core.Entities.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

[Table("UsrFeedbackAnswers")]
public class FeedbackAnswerEntity : BaseEntity
{
    public string? Text { get; set; }
    public virtual UserEntity? Owner { get; set; }
    public virtual FeedbackEntity? Feedback { get; set; }
}
