using Core.Entities.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

[Table("UsrFeedbacks")]
public class FeedbackEntity : BaseEntity
{
    public string? Text { get; set; }
    public virtual UserEntity? Owner { get; set; }
    public virtual ProductEntity? Product { get; set; }
    public virtual ICollection<FeedbackAnswerEntity> Answers { get; set; } = new HashSet<FeedbackAnswerEntity>();
}
