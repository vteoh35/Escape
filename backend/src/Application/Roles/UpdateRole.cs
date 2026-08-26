using Business_Logic.Employees;

namespace Application.Roles;

/// <summary>
/// Updates a role's name. Returns null if the id doesn't exist.
/// </summary>
public class UpdateRole
{
    private readonly IRoleRepository _roleRepository;

    public UpdateRole(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public Role? Execute(int roleId, string roleName)
    {
        var role = _roleRepository.GetById(roleId);

        if (role == null)
        {
            return null;
        }

        role.RoleName = roleName;

        _roleRepository.Update(role);

        return role;
    }
}
