namespace Application.Tasks;

/// <summary>
/// Deletes a task. Returns false if the id doesn't exist.
/// </summary>
public class DeleteTask
{
    private readonly ITaskRepository _taskRepository;

    public DeleteTask(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public bool Execute(string taskId)
    {
        var task = _taskRepository.GetById(taskId);

        if (task == null)
        {
            return false;
        }

        _taskRepository.Delete(task);

        return true;
    }
}