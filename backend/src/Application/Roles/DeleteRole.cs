namespace Application.Roles;

public class DeleteRole
{
    private readonly IRoleRepository _roleRepository;

    public DeleteRole(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public bool Execute(int roleId)
    {
        var role = _roleRepository.GetById(roleId);

        if (role == null)
        {
            return false;
        }

        _roleRepository.Delete(role);

        return true;
    }
}
