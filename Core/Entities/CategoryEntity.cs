using Core.Entities.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

[Table("UsrCategories")]
public class CategoryEntity : BaseEntity
{
    // properties
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? Image { get; set; }
    public string? Icon { get; set; }
    public Guid? ParentId { get; set; }

    // navigation properies
    public virtual ICollection<ProductEntity> Products { get; set; } = new HashSet<ProductEntity>();
    public virtual ICollection<CategoryEntity> Children { get; set; } = new HashSet<CategoryEntity>();
    public virtual ICollection<FilterNameEntity> FilterNames { get; set; } = new HashSet<FilterNameEntity>();
    public virtual CategoryEntity? Parent { get; set; }
}
