using Business_Logic.Tags;

namespace Application.Tags;

/// <summary>
/// Data access contract for project-tag assignments (ProjectTag), implemented in Infrastructure against Postgres.
/// </summary>
public interface IProjectTagRepository
{
    List<ProjectTag> GetByProjectId(string projectId);
    ProjectTag? Get(string projectId, int tagId);
    void Add(ProjectTag projectTag);
    void Delete(ProjectTag projectTag);
}
