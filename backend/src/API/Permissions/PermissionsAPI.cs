using Application.Permissions;

namespace API.Permissions;

/// <summary>
/// Permission endpoints: /permissions.
/// </summary>
public static class PermissionsAPI
{
    public static void MapPermissionEndpoints(this WebApplication app)
    {
        app.MapGet("/permissions", (GetPermission getPermission) => Results.Ok(getPermission.GetAll()));

        app.MapGet("/permissions/{id}", (int id, GetPermission getPermission) =>
        {
            var permission = getPermission.GetById(id);
            return permission == null ? Results.NotFound() : Results.Ok(permission);
        });

        app.MapPost("/permissions", (CreatePermissionRequest request, CreatePermission createPermission) =>
        {
            var permission = createPermission.Execute(request.PermissionName);
            return Results.Created($"/permissions/{permission.PermissionId}", permission);
        });

        app.MapPut("/permissions/{id}", (int id, CreatePermissionRequest request, UpdatePermission updatePermission) =>
        {
            var permission = updatePermission.Execute(id, request.PermissionName);
            return permission == null ? Results.NotFound() : Results.Ok(permission);
        });

        app.MapDelete("/permissions/{id}", (int id, DeletePermission deletePermission) =>
        {
            var deleted = deletePermission.Execute(id);
            return deleted ? Results.NoContent() : Results.NotFound();
        });
    }
}

public record CreatePermissionRequest(string PermissionName);
