using Business_Logic.Projects;

namespace Application.Projects;

/// <summary>
/// Data access contract for Projects, implemented in Infrastructure against Postgres.
/// </summary>
public interface IProjectRepository
{
    List<Project> GetAll();
    Project? GetById(string projectId);
    void Add(Project project);
    void Update(Project project);
    void Delete(Project project);
}
