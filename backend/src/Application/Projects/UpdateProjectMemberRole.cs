using Business_Logic.Projects;

namespace Application.Projects;

public class UpdateProjectMemberRole
{
    private readonly IProjectMemberRepository _projectMemberRepository;

    public UpdateProjectMemberRole(IProjectMemberRepository projectMemberRepository)
    {
        _projectMemberRepository = projectMemberRepository;
    }

    public ProjectMember? Execute(string employeeId, string projectId, string? role)
    {
        var projectMember = _projectMemberRepository.Get(employeeId, projectId);

        if (projectMember == null)
        {
            return null;
        }

        projectMember.Role = role;

        _projectMemberRepository.Update(projectMember);

        return projectMember;
    }
}
