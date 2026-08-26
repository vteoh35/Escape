using Business_Logic.Tasks;

namespace Application.Tasks;

/// <summary>
/// Updates a task's name. Returns null if the id doesn't exist.
/// </summary>
public class UpdateTask
{
    private readonly ITaskRepository _taskRepository;

    public UpdateTask(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public TaskItem? Execute(string taskId, string name)
    {
        var task = _taskRepository.GetById(taskId);

        if (task == null)
        {
            return null;
        }

        task.Name = name;

        _taskRepository.Update(task);

        return task;
    }
}