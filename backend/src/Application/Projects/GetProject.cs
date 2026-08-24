using Business_Logic.Projects;

namespace Application.Projects;

public class GetProject
{
    private readonly IProjectRepository _projectRepository;

    public GetProject(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public List<Project> GetAll()
    {
        return _projectRepository.GetAll();
    }

    public Project? GetById(string projectId)
    {
        return _projectRepository.GetById(projectId);
    }
}
