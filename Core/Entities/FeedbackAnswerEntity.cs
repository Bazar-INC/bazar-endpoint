
using Core.Entities.Abstract;

namespace Core.Entities;

public class FeedbackAnswerEntity : BaseEntity
{
    public string? Text { get; set; }
    public virtual UserEntity? Owner { get; set; }
    public virtual ProductEntity? Product { get; set; }
    public virtual FeedbackEntity? Feedback { get; set; }
}
