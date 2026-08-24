using Application.Tags;
using Business_Logic.Tags;
using Infrastructure.Database;

namespace Infrastructure.Tags;

public class ProjectTagRepository : IProjectTagRepository
{
    private readonly AppDbContext _context;

    public ProjectTagRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<ProjectTag> GetByProjectId(string projectId)
    {
        return _context.ProjectTags.Where(pt => pt.ProjectId == projectId).ToList();
    }

    public ProjectTag? Get(string projectId, int tagId)
    {
        return _context.ProjectTags.FirstOrDefault(pt => pt.ProjectId == projectId && pt.TagId == tagId);
    }

    public void Add(ProjectTag projectTag)
    {
        _context.ProjectTags.Add(projectTag);
        _context.SaveChanges();
    }

    public void Delete(ProjectTag projectTag)
    {
        _context.ProjectTags.Remove(projectTag);
        _context.SaveChanges();
    }
}
