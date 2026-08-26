using Business_Logic.ActivityLogs;

namespace Application.ActivityLogs;

/// <summary>
/// Data access contract for ActivityLog entries, implemented in Infrastructure against Postgres.
/// </summary>
public interface IActivityLogRepository
{
    List<ActivityLog> GetAll();
    ActivityLog? GetById(string logId);
    void Add(ActivityLog activityLog);
    void Update(ActivityLog activityLog);
    void Delete(ActivityLog activityLog);
}
