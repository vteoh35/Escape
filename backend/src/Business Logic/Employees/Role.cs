namespace Business_Logic.Employees;

/// <summary>
/// A named RBAC role (e.g. "Manager") that groups a set of Permissions via RolePermission. Admin-defined, not a fixed enum.
/// </summary>
public class Role
{
    public int RoleId { get; set; }
    public string RoleName { get; set; }
}
