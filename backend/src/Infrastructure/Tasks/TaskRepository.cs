using Application.Tasks;
using Business_Logic.Tasks;

namespace Infrastructure.Tasks;

public class TaskRepository : ITaskRepository
{
    private readonly List<TaskItem> _tasks = new();

    public List<TaskItem> GetAll()
    {
        return _tasks;
    }

    public TaskItem? GetById(string taskId)
    {
        return _tasks.FirstOrDefault(task => task.TaskId == taskId);
    }

    public void Add(TaskItem task)
    {
        _tasks.Add(task);
    }

    public void Update(TaskItem task)
    {
        // Nothing required yet because the task object
        // already exists inside the list.
    }

    public void Delete(TaskItem task)
    {
        _tasks.Remove(task);
    }
}