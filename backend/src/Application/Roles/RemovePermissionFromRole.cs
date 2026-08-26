namespace Application.Roles;

/// <summary>
/// Revokes a permission from a role. Returns false if it wasn't granted.
/// </summary>
public class RemovePermissionFromRole
{
    private readonly IRolePermissionRepository _rolePermissionRepository;

    public RemovePermissionFromRole(IRolePermissionRepository rolePermissionRepository)
    {
        _rolePermissionRepository = rolePermissionRepository;
    }

    public bool Execute(int roleId, int permissionId)
    {
        var rolePermission = _rolePermissionRepository.Get(roleId, permissionId);

        if (rolePermission == null)
        {
            return false;
        }

        _rolePermissionRepository.Delete(rolePermission);

        return true;
    }
}
