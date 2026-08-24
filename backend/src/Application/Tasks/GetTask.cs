using Business_Logic.Tasks;

namespace Application.Tasks;

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