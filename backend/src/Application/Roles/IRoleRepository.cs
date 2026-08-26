using Business_Logic.Employees;

namespace Application.Roles;

/// <summary>
/// Data access contract for Roles, implemented in Infrastructure against Postgres.
/// </summary>
public interface IRoleRepository
{
    List<Role> GetAll();
    Role? GetById(int roleId);
    void Add(Role role);
    void Update(Role role);
    void Delete(Role role);
}
