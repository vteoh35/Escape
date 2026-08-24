using Application.Attachments;
using Business_Logic.Attachments;
using Infrastructure.Database;

namespace Infrastructure.Attachments;

public class AttachmentsRepository : IAttachmentRepository
{
    private readonly AppDbContext _context;

    public AttachmentsRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<Attachment> GetAll()
    {
        return _context.Attachments.ToList();
    }

    public Attachment? GetById(string attachmentId)
    {
        return _context.Attachments.FirstOrDefault(attachment => attachment.AttachmentId == attachmentId);
    }

    public void Add(Attachment attachment)
    {
        _context.Attachments.Add(attachment);
        _context.SaveChanges();
    }

    public void Update(Attachment attachment)
    {
        _context.SaveChanges();
    }

    public void Delete(Attachment attachment)
    {
        _context.Attachments.Remove(attachment);
        _context.SaveChanges();
    }
}
