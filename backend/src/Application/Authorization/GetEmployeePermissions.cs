using Application.Employees;
using Application.Permissions;
using Application.Roles;

namespace Application.Authorization;

public class GetEmployeePermissions
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IRolePermissionRepository _rolePermissionRepository;
    private readonly IPermissionRepository _permissionRepository;

    public GetEmployeePermissions(
        IEmployeeRepository employeeRepository,
        IRolePermissionRepository rolePermissionRepository,
        IPermissionRepository permissionRepository)
    {
        _employeeRepository = employeeRepository;
        _rolePermissionRepository = rolePermissionRepository;
        _permissionRepository = permissionRepository;
    }

    public List<string> Execute(string employeeId)
    {
        var employee = _employeeRepository.GetById(employeeId);

        if (employee?.RoleId == null)
        {
            return new List<string>();
        }

        var rolePermissions = _rolePermissionRepository.GetByRoleId(employee.RoleId.Value);

        var permissionNames = new List<string>();

        foreach (var rolePermission in rolePermissions)
        {
            var permission = _permissionRepository.GetById(rolePermission.PermissionId);

            if (permission != null)
            {
                permissionNames.Add(permission.PermissionName);
            }
        }

        return permissionNames;
    }
}
