using Business_Logic.Employees;

namespace Application.Permissions;

public class UpdatePermission
{
    private readonly IPermissionRepository _permissionRepository;

    public UpdatePermission(IPermissionRepository permissionRepository)
    {
        _permissionRepository = permissionRepository;
    }

    public Permission? Execute(int permissionId, string permissionName)
    {
        var permission = _permissionRepository.GetById(permissionId);

        if (permission == null)
        {
            return null;
        }

        permission.PermissionName = permissionName;

        _permissionRepository.Update(permission);

        return permission;
    }
}
