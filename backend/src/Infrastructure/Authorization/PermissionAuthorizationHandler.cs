using System.Security.Claims;
using Application.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Authorization;

/// <summary>
/// Evaluates a PermissionRequirement: reads the caller's employee id from the JWT, resolves their permissions via GetEmployeePermissions, and succeeds if the required permission is present.
/// </summary>
public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly GetEmployeePermissions _getEmployeePermissions;

    public PermissionAuthorizationHandler(GetEmployeePermissions getEmployeePermissions)
    {
        _getEmployeePermissions = getEmployeePermissions;
    }

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        var employeeId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (employeeId != null)
        {
            var permissions = _getEmployeePermissions.Execute(employeeId);

            if (permissions.Contains(requirement.Permission))
            {
                context.Succeed(requirement);
            }
        }

        return Task.CompletedTask;
    }
}
