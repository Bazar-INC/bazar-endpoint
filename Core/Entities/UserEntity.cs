using Core.Entities.Abstract;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities;

public class UserEntity : IdentityUser<Guid>, IEntity
{
    // properties
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}