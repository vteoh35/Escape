using Business_Logic.Tasks;

namespace Application.Tasks;

/// <summary>
/// Data access contract for Tasks, implemented in Infrastructure against Postgres.
/// </summary>
public interface ITaskRepository
{
    List<TaskItem> GetAll();
    TaskItem? GetById(string taskId);
    void Add(TaskItem task);
    void Update(TaskItem task);
    void Delete(TaskItem task);
}