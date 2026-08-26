namespace Business_Logic.Employees;

/// <summary>
/// A single named permission (e.g. "edit_project") that can be granted to a Role. Admin-defined, not a fixed enum.
/// </summary>
public class Permission
{
    public int PermissionId { get; set; }
    public string PermissionName { get; set; }
}
