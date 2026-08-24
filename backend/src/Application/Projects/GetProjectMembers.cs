using Business_Logic.Projects;

namespace Application.Projects;

public class GetProjectMembers
{
    private readonly IProjectMemberRepository _projectMemberRepository;

    public GetProjectMembers(IProjectMemberRepository projectMemberRepository)
    {
        _projectMemberRepository = projectMemberRepository;
    }

    public List<ProjectMember> Execute(string projectId)
    {
        return _projectMemberRepository.GetByProjectId(projectId);
    }
}
