
using Core.Entities.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

[Table("UsrImages")]
public class ImageEntity : BaseEntity
{
    // properties
    public string? Path { get; set; }
    public int Order { get; set; }

    // navigation properties
    public virtual ProductEntity? Product { get; set; }
}