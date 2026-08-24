using Business_Logic.Tasks;

namespace Application.Tasks;

public interface ITaskAssigneeRepository
{
    List<TaskAssignee> GetByTaskId(string taskId);
    TaskAssignee? Get(string employeeId, string taskId);
    void Add(TaskAssignee taskAssignee);
    void Update(TaskAssignee taskAssignee);
    void Delete(TaskAssignee taskAssignee);
}
