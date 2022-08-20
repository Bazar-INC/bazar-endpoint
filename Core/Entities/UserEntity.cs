using Core.Entities.Abstract;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities;

public class UserEntity : IdentityUser<Guid>, IEntity
{
    // properties
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string? Image { get; set; }

    public virtual ICollection<FeedbackEntity> Feedbacks { get; set; } = new HashSet<FeedbackEntity>();
    public virtual ICollection<FeedbackAnswerEntity> FeedbackAnswers { get; set; } = new HashSet<FeedbackAnswerEntity>();
    public virtual ICollection<QuestionEntity> Questions { get; set; } = new HashSet<QuestionEntity>();
    public virtual ICollection<QuestionAnswerEntity> QuestionAnswers { get; set; } = new HashSet<QuestionAnswerEntity>();
}