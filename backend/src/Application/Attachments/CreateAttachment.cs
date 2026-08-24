using Business_Logic.Attachments;

namespace Application.Attachments;

public class CreateAttachment
{
    private readonly IAttachmentRepository _attachmentRepository;

    public CreateAttachment(IAttachmentRepository attachmentRepository)
    {
        _attachmentRepository = attachmentRepository;
    }

    public Attachment Execute(string attachmentId, string attachmentLocation, string? projectId, string? taskId)
    {
        var attachment = new Attachment
        {
            AttachmentId = attachmentId,
            AttachmentLocation = attachmentLocation,
            ProjectId = projectId,
            TaskId = taskId
        };

        _attachmentRepository.Add(attachment);

        return attachment;
    }
}
