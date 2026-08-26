namespace Business_Logic.Projects;

/// <summary>
/// Join entity assigning an Employee to a Project, with an optional free-text role label (e.g. "Project Manager") -- unrelated to the RBAC Role entity.
/// </summary>
public class ProjectMember
{
    public string EmployeeId { get; set; }
    public string ProjectId { get; set; }
    public string? Role { get; set; }
}