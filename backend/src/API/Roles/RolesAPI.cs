using Application.Roles;

namespace API.Roles;

public static class RolesAPI
{
    public static void MapRoleEndpoints(this WebApplication app)
    {
        app.MapGet("/roles", (GetRole getRole) => Results.Ok(getRole.GetAll()));

        app.MapGet("/roles/{id}", (int id, GetRole getRole) =>
        {
            var role = getRole.GetById(id);
            return role == null ? Results.NotFound() : Results.Ok(role);
        });

        app.MapPost("/roles", (CreateRoleRequest request, CreateRole createRole) =>
        {
            var role = createRole.Execute(request.RoleName);
            return Results.Created($"/roles/{role.RoleId}", role);
        });

        app.MapPut("/roles/{id}", (int id, CreateRoleRequest request, UpdateRole updateRole) =>
        {
            var role = updateRole.Execute(id, request.RoleName);
            return role == null ? Results.NotFound() : Results.Ok(role);
        });

        app.MapDelete("/roles/{id}", (int id, DeleteRole deleteRole) =>
        {
            var deleted = deleteRole.Execute(id);
            return deleted ? Results.NoContent() : Results.NotFound();
        });

        app.MapGet("/roles/{id}/permissions", (int id, GetRolePermissions getRolePermissions) =>
            Results.Ok(getRolePermissions.Execute(id)));

        app.MapPost("/roles/{id}/permissions/{permissionId}", (int id, int permissionId, AssignPermissionToRole assign) =>
        {
            var rolePermission = assign.Execute(id, permissionId);
            return Results.Created($"/roles/{id}/permissions/{permissionId}", rolePermission);
        });

        app.MapDelete("/roles/{id}/permissions/{permissionId}", (int id, int permissionId, RemovePermissionFromRole remove) =>
        {
            var removed = remove.Execute(id, permissionId);
            return removed ? Results.NoContent() : Results.NotFound();
        });
    }
}

public record CreateRoleRequest(string RoleName);
