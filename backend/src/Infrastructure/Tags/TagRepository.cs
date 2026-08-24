using Application.Tags;
using Business_Logic.Tags;
using Infrastructure.Database;

namespace Infrastructure.Tags;

public class TagRepository : ITagRepository
{
    private readonly AppDbContext _context;

    public TagRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<Tag> GetAll()
    {
        return _context.Tags.ToList();
    }

    public Tag? GetById(int tagId)
    {
        return _context.Tags.FirstOrDefault(tag => tag.TagId == tagId);
    }

    public void Add(Tag tag)
    {
        _context.Tags.Add(tag);
        _context.SaveChanges();
    }

    public void Update(Tag tag)
    {
        _context.SaveChanges();
    }

    public void Delete(Tag tag)
    {
        _context.Tags.Remove(tag);
        _context.SaveChanges();
    }
}
