namespace Business_Logic.Tags;

/// <summary>
/// Join entity applying a Tag to a Project (many-to-many).
/// </summary>
public class ProjectTag
{
    public string ProjectId { get; set; }
    public int TagId { get; set; }
}
