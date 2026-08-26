using Business_Logic.Employees;

namespace Application.Permissions;

/// <summary>
/// Creates a new permission. PermissionId is database-generated, so callers only supply the name.
/// </summary>
public class CreatePermission
{
    private readonly IPermissionRepository _permissionRepository;

    public CreatePermission(IPermissionRepository permissionRepository)
    {
        _permissionRepository = permissionRepository;
    }

    public Permission Execute(string permissionName)
    {
        var permission = new Permission
        {
            PermissionName = permissionName
        };

        _permissionRepository.Add(permission);

        return permission;
    }
}
