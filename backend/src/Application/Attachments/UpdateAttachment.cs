using Business_Logic.Attachments;

namespace Application.Attachments;

public class UpdateAttachment
{
    private readonly IAttachmentRepository _attachmentRepository;

    public UpdateAttachment(IAttachmentRepository attachmentRepository)
    {
        _attachmentRepository = attachmentRepository;
    }

    public Attachment? Execute(string attachmentId, string attachmentLocation)
    {
        var attachment = _attachmentRepository.GetById(attachmentId);

        if (attachment == null)
        {
            return null;
        }

        attachment.AttachmentLocation = attachmentLocation;

        _attachmentRepository.Update(attachment);

        return attachment;
    }
}
