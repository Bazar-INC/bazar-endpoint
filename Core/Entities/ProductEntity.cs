using Core.Entities.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

[Table("UsrProducts")]
public class ProductEntity : BaseEntity
{
    // properties
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public string? Description { get; set; }

    // foreign keys
    public Guid CategoryId { get; set; }

    // navigation properties
    public virtual CategoryEntity? Category { get; set; }
    public virtual ICollection<FilterValueEntity> FilterValues { get; set; } = new HashSet<FilterValueEntity>();
    public virtual ICollection<ImageEntity> Images { get; set; } = new HashSet<ImageEntity>();
    public virtual ICollection<FeedbackEntity> Feedbacks { get; set; } = new HashSet<FeedbackEntity>();
    public virtual ICollection<FeedbackAnswerEntity> FeedbackAnswers { get; set; } = new HashSet<FeedbackAnswerEntity>();
    public virtual ICollection<QuestionEntity> Questions { get; set; } = new HashSet<QuestionEntity>();
    public virtual ICollection<QuestionAnswerEntity> QuestionAnswers { get; set; } = new HashSet<QuestionAnswerEntity>();
}