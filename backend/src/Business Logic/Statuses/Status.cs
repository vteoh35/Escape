namespace Business_Logic.Statuses;

/// <summary>
/// A workflow status (e.g. "In Progress") referenced by Task/Project.StatusId. Small, admin-configurable lookup table.
/// </summary>
public class Status
{
    public int StatusId { get; set; }
    public string StatusName { get; set; }
}