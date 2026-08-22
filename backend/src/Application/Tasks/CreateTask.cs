using Business_Logic.Tasks;

namespace Application.Tasks;

public class CreateTask
{
    private readonly ITaskRepository _taskRepository;

    public CreateTask(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public TaskItem Execute(string name)
    {
        var tasks = _taskRepository.GetAll();

        int newId = tasks.Count == 0
            ? 1
            : tasks.Max(task => task.Id) + 1;

        var task = new TaskItem(newId, name);

        _taskRepository.Add(task);

        return task;
    }
}