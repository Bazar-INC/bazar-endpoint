using Microsoft.AspNetCore.Authorization;
using Shared;

namespace Web.AuthorizeAttributes;

/// <summary>
/// custom [AuthorizeAdmin] authorize attribute
/// that gives access to methods to users who have Admin role
/// </summary>
public class AuthorizeAdminAttribute : AuthorizeAttribute
{
    public AuthorizeAdminAttribute()
    {
        base.Roles = AppSettings.Roles.Admin;
    }
}
