using Business_Logic.Attachments;

namespace Application.Attachments;

/// <summary>
/// Data access contract for Attachments, implemented in Infrastructure against Postgres.
/// </summary>
public interface IAttachmentRepository
{
    List<Attachment> GetAll();
    Attachment? GetById(string attachmentId);
    void Add(Attachment attachment);
    void Update(Attachment attachment);
    void Delete(Attachment attachment);
}
