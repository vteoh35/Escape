using Business_Logic.Projects;

namespace Application.Projects;

public interface IProjectMemberRepository
{
    List<ProjectMember> GetByProjectId(string projectId);
    ProjectMember? Get(string employeeId, string projectId);
    void Add(ProjectMember projectMember);
    void Update(ProjectMember projectMember);
    void Delete(ProjectMember projectMember);
}
