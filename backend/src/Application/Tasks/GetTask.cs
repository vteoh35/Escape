using Business_Logic.Tasks;

namespace Application.Tasks;

/// <summary>
/// Reads tasks, either all of them or by id.
/// </summary>
public class GetTask
{
    private readonly ITaskRepository _taskRepository;

    public GetTask(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public List<TaskItem> GetAll()
    {
        return _taskRepository.GetAll();
    }

    public TaskItem? GetById(string taskId)
    {
        return _taskRepository.GetById(taskId);
    }
}