using Business_Logic.Projects;

namespace Application.Projects;

public class AddProjectMember
{
    private readonly IProjectMemberRepository _projectMemberRepository;

    public AddProjectMember(IProjectMemberRepository projectMemberRepository)
    {
        _projectMemberRepository = projectMemberRepository;
    }

    public ProjectMember Execute(string employeeId, string projectId, string? role)
    {
        var projectMember = new ProjectMember
        {
            EmployeeId = employeeId,
            ProjectId = projectId,
            Role = role
        };

        _projectMemberRepository.Add(projectMember);

        return projectMember;
    }
}
