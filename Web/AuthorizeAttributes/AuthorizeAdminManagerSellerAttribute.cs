using Microsoft.AspNetCore.Authorization;
using Shared;

namespace Web.AuthorizeAttributes;

/// <summary>
/// custom [AuthorizeAdminManagerSeller] authorize attribute
/// that gives access to methods for users who have Admin or Manager or Seller role
/// </summary>
public class AuthorizeAdminManagerSellerAttribute : AuthorizeAttribute
{
    public AuthorizeAdminManagerSellerAttribute()
    {
        base.Roles = AppSettings.Roles.Admin + ", " + AppSettings.Roles.Manager;
    }
}