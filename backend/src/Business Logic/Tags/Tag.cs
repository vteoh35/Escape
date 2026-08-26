namespace Business_Logic.Tags;

/// <summary>
/// A short label (e.g. "urgent") that can be applied to both Projects and Tasks.
/// </summary>
public class Tag
{
    public int TagId { get; set; }
    public string TagName { get; set; }
}
