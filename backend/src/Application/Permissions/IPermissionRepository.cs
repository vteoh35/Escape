using Business_Logic.Employees;

namespace Application.Permissions;

public interface IPermissionRepository
{
    List<Permission> GetAll();
    Permission? GetById(int permissionId);
    void Add(Permission permission);
    void Update(Permission permission);
    void Delete(Permission permission);
}
