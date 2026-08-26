using Business_Logic.ActivityLogs;

namespace Application.ActivityLogs;

/// <summary>
/// Reads activity log entries, either all of them or by id.
/// </summary>
public class GetActivityLog
{
    private readonly IActivityLogRepository _activityLogRepository;

    public GetActivityLog(IActivityLogRepository activityLogRepository)
    {
        _activityLogRepository = activityLogRepository;
    }

    public List<ActivityLog> GetAll()
    {
        return _activityLogRepository.GetAll();
    }

    public ActivityLog? GetById(string logId)
    {
        return _activityLogRepository.GetById(logId);
    }
}
