using Core.Entities.Abstract;

namespace Core.Entities;

public class ProductEntity : BaseEntity
{
    // props
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }

    // nav props
    public virtual CategoryEntity? Category { get; set; }
}