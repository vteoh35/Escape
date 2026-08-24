using Business_Logic.Employees;

namespace Application.Roles;

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
