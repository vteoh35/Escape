namespace Application.Tasks;

public class UnassignEmployeeFromTask
{
    private readonly ITaskAssigneeRepository _taskAssigneeRepository;

    public UnassignEmployeeFromTask(ITaskAssigneeRepository taskAssigneeRepository)
    {
        _taskAssigneeRepository = taskAssigneeRepository;
    }

    public bool Execute(string employeeId, string taskId)
    {
        var taskAssignee = _taskAssigneeRepository.Get(employeeId, taskId);

        if (taskAssignee == null)
        {
            return false;
        }

        _taskAssigneeRepository.Delete(taskAssignee);

        return true;
    }
}
