namespace Business_Logic.Attachments;

/// <summary>
/// A file/link reference attached to a project or a task. Only the location (URL or path) is stored -- no upload/storage handling exists yet.
/// </summary>
public class Attachment
{
    public string AttachmentId { get; set; }
    public string AttachmentLocation { get; set; }
    public string? ProjectId { get; set; }
    public string? TaskId { get; set; }
}