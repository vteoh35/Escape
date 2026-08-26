using Business_Logic.Projects;

namespace Application.Projects;

/// <summary>
/// Updates a project member's role label. Returns null if the employee isn't a member of the project.
/// </summary>
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
