using Business_Logic.Employees;

namespace Application.Permissions;

/// <summary>
/// Data access contract for Permissions, implemented in Infrastructure against Postgres.
/// </summary>
public interface IPermissionRepository
{
    List<Permission> GetAll();
    Permission? GetById(int permissionId);
    void Add(Permission permission);
    void Update(Permission permission);
    void Delete(Permission permission);
}
