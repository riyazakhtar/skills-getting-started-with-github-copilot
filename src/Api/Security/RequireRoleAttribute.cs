using Microsoft.AspNetCore.Authorization;

namespace Api.Security;

// to simplify passing multiple roles to [Authorize]
public class RequireRoleAttribute : AuthorizeAttribute
{
    public RequireRoleAttribute(params string[] roles)
    {
        Roles = String.Join(",", roles);
    }
}
