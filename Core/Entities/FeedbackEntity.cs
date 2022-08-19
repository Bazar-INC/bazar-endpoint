using Core.Entities.Abstract;

namespace Core.Entities;

public class FeedbackEntity : BaseEntity
{
    public string? Text { get; set; }
    public virtual UserEntity? Owner { get; set; }
    public virtual ICollection<FeedbackAnswerEntity> Answers { get; set; } = new HashSet<FeedbackAnswerEntity>();
}
