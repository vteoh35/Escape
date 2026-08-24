using Business_Logic.Employees;

namespace Application.Roles;

public class GetRole
{
    private readonly IRoleRepository _roleRepository;

    public GetRole(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public List<Role> GetAll()
    {
        return _roleRepository.GetAll();
    }

    public Role? GetById(int roleId)
    {
        return _roleRepository.GetById(roleId);
    }
}
