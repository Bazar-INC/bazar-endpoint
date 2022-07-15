using Core.Entities.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

[Table("UsrFilterNames")]
public class FilterNameEntity : BaseEntity
{
    // props
    public string? Name { get; set; }
    public string? Code { get; set; }

    // nav props
    public virtual ICollection<FilterValueEntity> FilterValues { get; set; }

    // init
    public FilterNameEntity()
    {
        FilterValues = new HashSet<FilterValueEntity>();
    }
}
