using Application.Tasks;
using Business_Logic.Tasks;
using Infrastructure.Database;

namespace Infrastructure.Tasks;

public class TaskAssigneeRepository : ITaskAssigneeRepository
{
    private readonly AppDbContext _context;

    public TaskAssigneeRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<TaskAssignee> GetByTaskId(string taskId)
    {
        return _context.TaskAssignees.Where(ta => ta.TaskId == taskId).ToList();
    }

    public TaskAssignee? Get(string employeeId, string taskId)
    {
        return _context.TaskAssignees
            .FirstOrDefault(ta => ta.EmployeeId == employeeId && ta.TaskId == taskId);
    }

    public void Add(TaskAssignee taskAssignee)
    {
        _context.TaskAssignees.Add(taskAssignee);
        _context.SaveChanges();
    }

    public void Update(TaskAssignee taskAssignee)
    {
        _context.SaveChanges();
    }

    public void Delete(TaskAssignee taskAssignee)
    {
        _context.TaskAssignees.Remove(taskAssignee);
        _context.SaveChanges();
    }
}
