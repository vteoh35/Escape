using Business_Logic.Employees;

namespace Application.Roles;

/// <summary>
/// Data access contract for role-permission assignments (RolePermission), implemented in Infrastructure against Postgres.
/// </summary>
public interface IRolePermissionRepository
{
    List<RolePermission> GetByRoleId(int roleId);
    RolePermission? Get(int roleId, int permissionId);
    void Add(RolePermission rolePermission);
    void Delete(RolePermission rolePermission);
}
