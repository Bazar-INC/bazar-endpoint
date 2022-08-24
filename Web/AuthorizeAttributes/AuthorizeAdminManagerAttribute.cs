using Microsoft.AspNetCore.Authorization;
using Shared;

namespace Web.AuthorizeAttributes;

/// <summary>
/// custom [AuthorizeAdminOrManager] authorize attribute
/// that gives access to methods for users who have Admin or Manager role
/// </summary>
public class AuthorizeAdminManagerAttribute : AuthorizeAttribute
{
    public AuthorizeAdminManagerAttribute()
    {
        base.Roles = AppSettings.Roles.Admin + ", " + AppSettings.Roles.Manager;
    }
}
