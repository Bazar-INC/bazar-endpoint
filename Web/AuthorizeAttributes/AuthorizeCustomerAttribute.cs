using Microsoft.AspNetCore.Authorization;
using Shared;

namespace Web.AuthorizeAttributes;

/// <summary>
/// custom [AuthorizeCustomer] authorize attribute
/// that gives access to methods to users who have Customer role
/// </summary>
public class AuthorizeCustomerAttribute : AuthorizeAttribute
{
    public AuthorizeCustomerAttribute()
    {
        base.Roles = AppSettings.Roles.Customer;
    }
}
