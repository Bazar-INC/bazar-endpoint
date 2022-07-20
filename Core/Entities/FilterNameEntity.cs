using Core.Entities.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

[Table("UsrFilterNames")]
public class FilterNameEntity : BaseEntity
{
    // properties
    public string? Name { get; set; }
    public string? Code { get; set; }

    // navigation properties
    public virtual ICollection<FilterValueEntity> FilterValues { get; set; } = new HashSet<FilterValueEntity>();
}
