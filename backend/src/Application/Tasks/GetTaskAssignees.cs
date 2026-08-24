using Business_Logic.Tasks;

namespace Application.Tasks;

public class GetTaskAssignees
{
    private readonly ITaskAssigneeRepository _taskAssigneeRepository;

    public GetTaskAssignees(ITaskAssigneeRepository taskAssigneeRepository)
    {
        _taskAssigneeRepository = taskAssigneeRepository;
    }

    public List<TaskAssignee> Execute(string taskId)
    {
        return _taskAssigneeRepository.GetByTaskId(taskId);
    }
}
