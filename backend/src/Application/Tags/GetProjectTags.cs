using Business_Logic.Tags;

namespace Application.Tags;

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
