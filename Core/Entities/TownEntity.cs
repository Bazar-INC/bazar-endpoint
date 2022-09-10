
using Core.Entities.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

[Table("UsrTowns")]
public class TownEntity : BaseEntity
{
    public string? Name { get; set; }
}
