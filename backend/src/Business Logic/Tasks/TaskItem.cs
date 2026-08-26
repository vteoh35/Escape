namespace Business_Logic.Tasks;

/// <summary>
/// A task: belongs to a Project, can have a parent task (sub-tasks), and carries its own priority/status independent of the project's.
/// </summary>
public class TaskItem
{
    public string TaskId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public int? PriorityId { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public int? StatusId { get; set; }
    public string? ProjectId { get; set; }
    public string? ParentTaskId { get; set; }
}