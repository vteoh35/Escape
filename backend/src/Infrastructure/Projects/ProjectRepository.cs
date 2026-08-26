using Application.Projects;
using Business_Logic.Projects;
using Infrastructure.Database;

namespace Infrastructure.Projects;

/// <summary>
/// EF Core-backed implementation of IProjectRepository.
/// </summary>
public class ProjectRepository : IProjectRepository
{
    private readonly AppDbContext _context;

    public ProjectRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<Project> GetAll()
    {
        return _context.Projects.ToList();
    }

    public Project? GetById(string projectId)
    {
        return _context.Projects.FirstOrDefault(project => project.ProjectID == projectId);
    }

    public void Add(Project project)
    {
        _context.Projects.Add(project);
        _context.SaveChanges();
    }

    public void Update(Project project)
    {
        _context.SaveChanges();
    }

    public void Delete(Project project)
    {
        _context.Projects.Remove(project);
        _context.SaveChanges();
    }
}
