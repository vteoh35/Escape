namespace Application.Roles;

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
