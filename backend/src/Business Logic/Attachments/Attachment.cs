namespace Business_Logic.Attachments;

public class Attachment
{
    public string AttachmentId { get; set; }
    public string AttachmentLocation { get; set; }
    public string? ProjectId { get; set; }
    public string? TaskId { get; set; }
}