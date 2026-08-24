using Business_Logic.Employees;

namespace Application.Roles;

public interface IRoleRepository
{
    List<Role> GetAll();
    Role? GetById(int roleId);
    void Add(Role role);
    void Update(Role role);
    void Delete(Role role);
}
