using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Authorization;

/// <summary>
/// An ASP.NET Core authorization requirement: "the caller must hold this permission." Attach to a route via .RequireAuthorization(policy => policy.Requirements.Add(new PermissionRequirement("..."))).
/// </summary>
public class PermissionRequirement : IAuthorizationRequirement
{
    public string Permission { get; }

    public PermissionRequirement(string permission)
    {
        Permission = permission;
    }
}
