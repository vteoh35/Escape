using Business_Logic.Projects;

namespace Application.Projects;

public interface IProjectRepository
{
    List<Project> GetAll();
    Project? GetById(string projectId);
    void Add(Project project);
    void Update(Project project);
    void Delete(Project project);
}
