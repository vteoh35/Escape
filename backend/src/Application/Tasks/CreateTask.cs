using Business_Logic.Tasks;

namespace Application.Tasks;

/// <summary>
/// Creates a new task.
/// </summary>
public class CreateTask
{
    private readonly ITaskRepository _taskRepository;

    public CreateTask(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public TaskItem Execute(string taskId, string name)
    {
        var task = new TaskItem
        {
            TaskId = taskId,
            Name = name
        };

        _taskRepository.Add(task);

        return task;
    }
}