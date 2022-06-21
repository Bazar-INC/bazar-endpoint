using Core.Entities.Abstract;

namespace Core.Entities;

public class CodeEntity : BaseEntity
{
    public string? PhoneNumber { get; set; }
    public string? Code { get; set; }
}
