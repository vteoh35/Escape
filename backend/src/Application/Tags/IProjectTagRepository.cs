using Business_Logic.Tags;

namespace Application.Tags;

public interface IProjectTagRepository
{
    List<ProjectTag> GetByProjectId(string projectId);
    ProjectTag? Get(string projectId, int tagId);
    void Add(ProjectTag projectTag);
    void Delete(ProjectTag projectTag);
}
