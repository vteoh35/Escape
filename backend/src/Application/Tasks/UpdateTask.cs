using Business_Logic.Tasks;

namespace Application.Tasks;

public class UpdateTask
{
    private readonly ITaskRepository _taskRepository;

    public UpdateTask(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public TaskItem? Execute(int id, string name, bool isCompleted)
    {
        var task = _taskRepository.GetById(id);

        if (task == null)
        {
            return null;
        }

        task.Update(name, isCompleted);

        _taskRepository.Update(task);

        return task;
    }
}