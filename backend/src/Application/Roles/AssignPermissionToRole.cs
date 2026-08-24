using Business_Logic.Employees;

namespace Application.Roles;

public class AssignPermissionToRole
{
    private readonly IRolePermissionRepository _rolePermissionRepository;

    public AssignPermissionToRole(IRolePermissionRepository rolePermissionRepository)
    {
        _rolePermissionRepository = rolePermissionRepository;
    }

    public RolePermission Execute(int roleId, int permissionId)
    {
        var rolePermission = new RolePermission
        {
            RoleId = roleId,
            PermissionId = permissionId
        };

        _rolePermissionRepository.Add(rolePermission);

        return rolePermission;
    }
}
