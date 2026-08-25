using System.Security.Claims;
using Application.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Authorization;

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
