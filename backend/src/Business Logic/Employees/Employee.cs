namespace Business_Logic.Employees;

/// <summary>
/// A person on the team: identity, department, position level, and their RBAC role.
/// </summary>
public class Employee
{
    public string EmployeeId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public int? EmployeeLevel { get; set; }
    public string? Department { get; set; }
    public int? RoleId { get; set; }
}