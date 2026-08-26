using Application.ActivityLogs;
using Business_Logic.ActivityLogs;
using Infrastructure.Database;

namespace Infrastructure.ActivityLogs;

/// <summary>
/// EF Core-backed implementation of IActivityLogRepository.
/// </summary>
public class ActivityLogRepository : IActivityLogRepository
{
    private readonly AppDbContext _context;

    public ActivityLogRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<ActivityLog> GetAll()
    {
        return _context.ActivityLogs.ToList();
    }

    public ActivityLog? GetById(string logId)
    {
        return _context.ActivityLogs.FirstOrDefault(activityLog => activityLog.LogId == logId);
    }

    public void Add(ActivityLog activityLog)
    {
        _context.ActivityLogs.Add(activityLog);
        _context.SaveChanges();
    }

    public void Update(ActivityLog activityLog)
    {
        _context.SaveChanges();
    }

    public void Delete(ActivityLog activityLog)
    {
        _context.ActivityLogs.Remove(activityLog);
        _context.SaveChanges();
    }
}
