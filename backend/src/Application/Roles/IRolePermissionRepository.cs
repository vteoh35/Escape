using Business_Logic.Employees;

namespace Application.Roles;

public interface IRolePermissionRepository
{
    List<RolePermission> GetByRoleId(int roleId);
    RolePermission? Get(int roleId, int permissionId);
    void Add(RolePermission rolePermission);
    void Delete(RolePermission rolePermission);
}
