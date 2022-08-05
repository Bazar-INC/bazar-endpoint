using Core.Entities.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

[Table("UsrCodes")]
public class CodeEntity : BaseEntity
{
    // properties
    public string? PhoneNumber { get; set; }
    public string? Code { get; set; }
}
