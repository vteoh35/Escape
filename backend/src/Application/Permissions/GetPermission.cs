using Business_Logic.Employees;

namespace Application.Permissions;

/// <summary>
/// Reads permissions, either all of them or by id.
/// </summary>
public class GetPermission
{
    private readonly IPermissionRepository _permissionRepository;

    public GetPermission(IPermissionRepository permissionRepository)
    {
        _permissionRepository = permissionRepository;
    }

    public List<Permission> GetAll()
    {
        return _permissionRepository.GetAll();
    }

    public Permission? GetById(int permissionId)
    {
        return _permissionRepository.GetById(permissionId);
    }
}
