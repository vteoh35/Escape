using Application.Tags;
using Business_Logic.Tags;
using Infrastructure.Database;

namespace Infrastructure.Tags;

/// <summary>
/// EF Core-backed implementation of ITaskTagRepository.
/// </summary>
public class TaskTagRepository : ITaskTagRepository
{
    private readonly AppDbContext _context;

    public TaskTagRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<TaskTag> GetByTaskId(string taskId)
    {
        return _context.TaskTags.Where(tt => tt.TaskId == taskId).ToList();
    }

    public TaskTag? Get(string taskId, int tagId)
    {
        return _context.TaskTags.FirstOrDefault(tt => tt.TaskId == taskId && tt.TagId == tagId);
    }

    public void Add(TaskTag taskTag)
    {
        _context.TaskTags.Add(taskTag);
        _context.SaveChanges();
    }

    public void Delete(TaskTag taskTag)
    {
        _context.TaskTags.Remove(taskTag);
        _context.SaveChanges();
    }
}
