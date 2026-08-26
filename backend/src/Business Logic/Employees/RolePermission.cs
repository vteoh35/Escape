namespace Business_Logic.Employees;

/// <summary>
/// Join entity linking a Role to a Permission it grants (many-to-many).
/// </summary>
public class RolePermission
{
    public int RoleId { get; set; }
    public int PermissionId { get; set; }
}
