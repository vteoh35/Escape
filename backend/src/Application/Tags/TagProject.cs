using Business_Logic.Tags;

namespace Application.Tags;

public class TagProject
{
    private readonly IProjectTagRepository _projectTagRepository;

    public TagProject(IProjectTagRepository projectTagRepository)
    {
        _projectTagRepository = projectTagRepository;
    }

    public ProjectTag Execute(string projectId, int tagId)
    {
        var projectTag = new ProjectTag
        {
            ProjectId = projectId,
            TagId = tagId
        };

        _projectTagRepository.Add(projectTag);

        return projectTag;
    }
}
