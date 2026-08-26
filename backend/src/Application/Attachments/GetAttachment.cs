using Business_Logic.Attachments;

namespace Application.Attachments;

/// <summary>
/// Reads attachments, either all of them or by id.
/// </summary>
public class GetAttachment
{
    private readonly IAttachmentRepository _attachmentRepository;

    public GetAttachment(IAttachmentRepository attachmentRepository)
    {
        _attachmentRepository = attachmentRepository;
    }

    public List<Attachment> GetAll()
    {
        return _attachmentRepository.GetAll();
    }

    public Attachment? GetById(string attachmentId)
    {
        return _attachmentRepository.GetById(attachmentId);
    }
}
