using Application.Projects;
using Business_Logic.Projects;
using Infrastructure.Database;

namespace Infrastructure.Projects;

/// <summary>
/// EF Core-backed implementation of IProjectMemberRepository.
/// </summary>
public class ProjectMemberRepository : IProjectMemberRepository
{
    private readonly AppDbContext _context;

    public ProjectMemberRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<ProjectMember> GetByProjectId(string projectId)
    {
        return _context.ProjectMembers.Where(pm => pm.ProjectId == projectId).ToList();
    }

    public ProjectMember? Get(string employeeId, string projectId)
    {
        return _context.ProjectMembers
            .FirstOrDefault(pm => pm.EmployeeId == employeeId && pm.ProjectId == projectId);
    }

    public void Add(ProjectMember projectMember)
    {
        _context.ProjectMembers.Add(projectMember);
        _context.SaveChanges();
    }

    public void Update(ProjectMember projectMember)
    {
        _context.SaveChanges();
    }

    public void Delete(ProjectMember projectMember)
    {
        _context.ProjectMembers.Remove(projectMember);
        _context.SaveChanges();
    }
}
