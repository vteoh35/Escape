namespace Application.Tags;

/// <summary>
/// Removes a tag from a project. Returns false if it wasn't applied.
/// </summary>
public class UntagProject
{
    private readonly IProjectTagRepository _projectTagRepository;

    public UntagProject(IProjectTagRepository projectTagRepository)
    {
        _projectTagRepository = projectTagRepository;
    }

    public bool Execute(string projectId, int tagId)
    {
        var projectTag = _projectTagRepository.Get(projectId, tagId);

        if (projectTag == null)
        {
            return false;
        }

        _projectTagRepository.Delete(projectTag);

        return true;
    }
}
