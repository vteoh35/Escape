namespace Application.Attachments;

/// <summary>
/// Deletes an attachment. Returns false if the id doesn't exist.
/// </summary>
public class DeleteAttachment
{
    private readonly IAttachmentRepository _attachmentRepository;

    public DeleteAttachment(IAttachmentRepository attachmentRepository)
    {
        _attachmentRepository = attachmentRepository;
    }

    public bool Execute(string attachmentId)
    {
        var attachment = _attachmentRepository.GetById(attachmentId);

        if (attachment == null)
        {
            return false;
        }

        _attachmentRepository.Delete(attachment);

        return true;
    }
}
