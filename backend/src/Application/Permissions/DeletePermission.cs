namespace Application.Permissions;

/// <summary>
/// Deletes a permission. Returns false if the id doesn't exist.
/// </summary>
public class DeletePermission
{
    private readonly IPermissionRepository _permissionRepository;

    public DeletePermission(IPermissionRepository permissionRepository)
    {
        _permissionRepository = permissionRepository;
    }

    public bool Execute(int permissionId)
    {
        var permission = _permissionRepository.GetById(permissionId);

        if (permission == null)
        {
            return false;
        }

        _permissionRepository.Delete(permission);

        return true;
    }
}
