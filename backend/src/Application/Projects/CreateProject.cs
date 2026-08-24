using Business_Logic.Projects;

namespace Application.Projects;

public class CreateProject
{
    private readonly IProjectRepository _projectRepository;

    public CreateProject(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public Project Execute(string projectId, string name)
    {
        var project = new Project
        {
            ProjectID = projectId,
            Name = name
        };

        _projectRepository.Add(project);

        return project;
    }
}
