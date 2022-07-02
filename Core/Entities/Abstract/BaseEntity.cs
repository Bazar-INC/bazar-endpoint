namespace Core.Entities.Abstract;

public abstract class BaseEntity : IEntity
{
    // key
    public Guid Id { get; set; }

    // props
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}