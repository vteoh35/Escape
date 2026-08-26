using Business_Logic.Employees;

namespace Application.Roles;

/// <summary>
/// Creates a new role. RoleId is database-generated, so callers only supply the name.
/// </summary>
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
