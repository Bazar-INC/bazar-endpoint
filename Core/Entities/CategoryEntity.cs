using Core.Entities.Abstract;

namespace Core.Entities;

public class CategoryEntity : BaseEntity
{
    // props
    public string? Title { get; set; }
    public string? Image { get; set; }
    public Guid? ParentId { get; set; }

    // nav props
    public virtual ICollection<ProductEntity> Products { get; set; }
    public virtual ICollection<CategoryEntity> Children { get; set; }
    public virtual CategoryEntity? Parent { get; set; }

    // init
    public CategoryEntity()
    {
        Products = new HashSet<ProductEntity>();
        Children = new HashSet<CategoryEntity>();
    }
}
