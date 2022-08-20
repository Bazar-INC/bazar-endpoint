using Core.Entities.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

[Table("UsrFeedbacks")]
public class FeedbackEntity : BaseEntity
{
    // properties
    public string? Text { get; set; }
    public int Stars { get; set; }
    public bool IsEdited { get; set; }

    // foreign keys
    public Guid OwnerId { get; set; }
    public Guid ProductId { get; set; }

    // navigation properties
    public virtual UserEntity? Owner { get; set; }
    public virtual ProductEntity? Product { get; set; }
    public virtual ICollection<FeedbackAnswerEntity> Answers { get; set; } = new HashSet<FeedbackAnswerEntity>();
}
