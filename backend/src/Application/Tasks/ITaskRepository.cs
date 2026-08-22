using Business_Logic.Tasks;

namespace Application.Tasks;

public interface ITaskRepository
{
    List<TaskItem> GetAll();
    TaskItem? GetById(int id);
    void Add(TaskItem task);
    void Update(TaskItem task);
    void Delete(TaskItem task);
}