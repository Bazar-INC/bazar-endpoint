using Microsoft.AspNetCore.Authorization;
using Shared;

namespace Web.AuthorizeAttributes;

public class AuthorizeCustomerAttribute : AuthorizeAttribute
{
    public AuthorizeCustomerAttribute()
    {
        base.Roles = AppSettings.Roles.Customer;
    }
}
