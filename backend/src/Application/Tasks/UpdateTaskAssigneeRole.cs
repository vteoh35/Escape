using Business_Logic.Tasks;

namespace Application.Tasks;

/// <summary>
/// Updates an assignee's role label on a task. Returns null if the employee isn't assigned to it.
/// </summary>
public class UpdateTaskAssigneeRole
{
    private readonly ITaskAssigneeRepository _taskAssigneeRepository;

    public UpdateTaskAssigneeRole(ITaskAssigneeRepository taskAssigneeRepository)
    {
        _taskAssigneeRepository = taskAssigneeRepository;
    }

    public TaskAssignee? Execute(string employeeId, string taskId, string? role)
    {
        var taskAssignee = _taskAssigneeRepository.Get(employeeId, taskId);

        if (taskAssignee == null)
        {
            return null;
        }

        taskAssignee.Role = role;

        _taskAssigneeRepository.Update(taskAssignee);

        return taskAssignee;
    }
}
