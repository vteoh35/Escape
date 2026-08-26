using Business_Logic.Projects;

namespace Application.Projects;

/// <summary>
/// Data access contract for project membership (ProjectMember), implemented in Infrastructure against Postgres.
/// </summary>
public interface IProjectMemberRepository
{
    List<ProjectMember> GetByProjectId(string projectId);
    ProjectMember? Get(string employeeId, string projectId);
    void Add(ProjectMember projectMember);
    void Update(ProjectMember projectMember);
    void Delete(ProjectMember projectMember);
}
