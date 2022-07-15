
using Core.Entities.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

[Table("UsrFilterValues")]
public class FilterValueEntity : BaseEntity
{
    // props
    public string? Value { get; set; }
    public string? Code { get; set; }

    // nav props
    public virtual FilterNameEntity? FilterName { get; set; }
    public virtual ICollection<CategoryEntity> Categories { get; set; }
    public virtual ICollection<ProductEntity> Products { get; set; }

    // init
    public FilterValueEntity()
    {
        Categories = new HashSet<CategoryEntity>();
        Products = new HashSet<ProductEntity>();
    }
}
