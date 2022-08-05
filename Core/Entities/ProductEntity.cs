using Core.Entities.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

[Table("UsrProducts")]
public class ProductEntity : BaseEntity
{
    // properties
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }

    // navigation properties
    public virtual CategoryEntity? Category { get; set; }
    public virtual ICollection<FilterValueEntity> FilterValues { get; set; } = new HashSet<FilterValueEntity>();
}