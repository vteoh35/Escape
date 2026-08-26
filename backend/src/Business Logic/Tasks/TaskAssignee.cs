namespace Business_Logic.Tasks;

/// <summary>
/// Join entity assigning an Employee to a Task, with an optional free-text role label (e.g. "Designer").
/// </summary>
public class TaskAssignee
{
    public string EmployeeId { get; set; }
    public string TaskId { get; set; }
    public string? Role { get; set; }
}