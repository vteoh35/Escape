namespace Business_Logic.Tags;

/// <summary>
/// Join entity applying a Tag to a Task (many-to-many).
/// </summary>
public class TaskTag
{
    public string TaskId { get; set; }
    public int TagId { get; set; }
}
