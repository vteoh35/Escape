using Business_Logic.Projects;

namespace Application.Projects;

public class UpdateProject
{
    private readonly IProjectRepository _projectRepository;

    public UpdateProject(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public Project? Execute(string projectId, string name)
    {
        var project = _projectRepository.GetById(projectId);

        if (project == null)
        {
            return null;
        }

        project.Name = name;

        _projectRepository.Update(project);

        return project;
    }
}
