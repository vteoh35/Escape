using Business_Logic.Tags;

namespace Application.Tags;

/// <summary>
/// Lists the tags applied to a project.
/// </summary>
public class GetProjectTags
{
    private readonly IProjectTagRepository _projectTagRepository;

    public GetProjectTags(IProjectTagRepository projectTagRepository)
    {
        _projectTagRepository = projectTagRepository;
    }

    public List<ProjectTag> Execute(string projectId)
    {
        return _projectTagRepository.GetByProjectId(projectId);
    }
}
