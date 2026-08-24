namespace Application.Projects;

public class DeleteProject
{
    private readonly IProjectRepository _projectRepository;

    public DeleteProject(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public bool Execute(string projectId)
    {
        var project = _projectRepository.GetById(projectId);

        if (project == null)
        {
            return false;
        }

        _projectRepository.Delete(project);

        return true;
    }
}
