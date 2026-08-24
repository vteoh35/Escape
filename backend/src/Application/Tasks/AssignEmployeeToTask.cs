using Business_Logic.Tasks;

namespace Application.Tasks;

public class AssignEmployeeToTask
{
    private readonly ITaskAssigneeRepository _taskAssigneeRepository;

    public AssignEmployeeToTask(ITaskAssigneeRepository taskAssigneeRepository)
    {
        _taskAssigneeRepository = taskAssigneeRepository;
    }

    public TaskAssignee Execute(string employeeId, string taskId, string? role)
    {
        var taskAssignee = new TaskAssignee
        {
            EmployeeId = employeeId,
            TaskId = taskId,
            Role = role
        };

        _taskAssigneeRepository.Add(taskAssignee);

        return taskAssignee;
    }
}
