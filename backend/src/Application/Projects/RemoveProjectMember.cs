namespace Application.Projects;

public class RemoveProjectMember
{
    private readonly IProjectMemberRepository _projectMemberRepository;

    public RemoveProjectMember(IProjectMemberRepository projectMemberRepository)
    {
        _projectMemberRepository = projectMemberRepository;
    }

    public bool Execute(string employeeId, string projectId)
    {
        var projectMember = _projectMemberRepository.Get(employeeId, projectId);

        if (projectMember == null)
        {
            return false;
        }

        _projectMemberRepository.Delete(projectMember);

        return true;
    }
}
