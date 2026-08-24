namespace Application.Tasks;

public class DeleteTask
{
    private readonly ITaskRepository _taskRepository;

    public DeleteTask(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public bool Execute(int id)
    {
        var task = _taskRepository.GetById(id);

        if (task == null)
        {
            return false;
        }

        _taskRepository.Delete(task);

        return true;
    }
}