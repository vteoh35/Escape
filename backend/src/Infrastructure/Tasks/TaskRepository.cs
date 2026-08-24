using Application.Tasks;
using Business_Logic.Tasks;
using Infrastructure.Database;

namespace Infrastructure.Tasks;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<TaskItem> GetAll()
    {
        return _context.Tasks.ToList();
    }

    public TaskItem? GetById(string taskId)
    {
        return _context.Tasks.FirstOrDefault(task => task.TaskId == taskId);
    }

    public void Add(TaskItem task)
    {
        _context.Tasks.Add(task);
        _context.SaveChanges();
    }

    public void Update(TaskItem task)
    {
        _context.SaveChanges();
    }

    public void Delete(TaskItem task)
    {
        _context.Tasks.Remove(task);
        _context.SaveChanges();
    }
}