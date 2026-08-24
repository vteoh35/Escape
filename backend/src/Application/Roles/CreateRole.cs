using Business_Logic.Employees;

namespace Application.Roles;

public class CreateRole
{
    private readonly IRoleRepository _roleRepository;

    public CreateRole(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public Role Execute(string roleName)
    {
        var role = new Role
        {
            RoleName = roleName
        };

        _roleRepository.Add(role);

        return role;
    }
}
