namespace Business_Logic.Priorities;

/// <summary>
/// A priority level (e.g. "High") referenced by Task/Project.PriorityId. Small, admin-configurable lookup table.
/// </summary>
public class Priority
{
    public int PriorityId { get; set; }
    public string PriorityName { get; set; }
}