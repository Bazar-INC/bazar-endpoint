
using Core.Entities.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

[Table("UsrFilterValues")]
public class FilterValueEntity : BaseEntity
{
    // properties
    public string? Value { get; set; }
    public string? Code { get; set; }

    // navigation properties
    public virtual FilterNameEntity? FilterName { get; set; }
    public virtual ICollection<CategoryEntity> Categories { get; set; } = new HashSet<CategoryEntity>();
    public virtual ICollection<ProductEntity> Products { get; set; } = new HashSet<ProductEntity>();
}
