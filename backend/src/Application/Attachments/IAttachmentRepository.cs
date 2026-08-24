using Business_Logic.Attachments;

namespace Application.Attachments;

public interface IAttachmentRepository
{
    List<Attachment> GetAll();
    Attachment? GetById(string attachmentId);
    void Add(Attachment attachment);
    void Update(Attachment attachment);
    void Delete(Attachment attachment);
}
