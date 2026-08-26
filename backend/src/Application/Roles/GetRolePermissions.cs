using Business_Logic.Employees;

namespace Application.Roles;

/// <summary>
/// Lists the permissions granted to a role.
/// </summary>
public class GetRolePermissions
{
    private readonly IRolePermissionRepository _rolePermissionRepository;

    public GetRolePermissions(IRolePermissionRepository rolePermissionRepository)
    {
        _rolePermissionRepository = rolePermissionRepository;
    }

    public List<RolePermission> Execute(int roleId)
    {
        return _rolePermissionRepository.GetByRoleId(roleId);
    }
}
