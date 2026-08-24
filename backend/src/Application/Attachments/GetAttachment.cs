using Business_Logic.Attachments;

namespace Application.Attachments;

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
