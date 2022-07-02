using Core.Entities.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

[Table("UsrProducts")]
public class ProductEntity : BaseEntity
{
    // props
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }

    // nav props
    public virtual CategoryEntity? Category { get; set; }
}