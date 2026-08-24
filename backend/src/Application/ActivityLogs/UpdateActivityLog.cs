using Business_Logic.ActivityLogs;

namespace Application.ActivityLogs;

public class UpdateActivityLog
{
    private readonly IActivityLogRepository _activityLogRepository;

    public UpdateActivityLog(IActivityLogRepository activityLogRepository)
    {
        _activityLogRepository = activityLogRepository;
    }

    public ActivityLog? Execute(string logId, string description)
    {
        var activityLog = _activityLogRepository.GetById(logId);

        if (activityLog == null)
        {
            return null;
        }

        activityLog.Description = description;

        _activityLogRepository.Update(activityLog);

        return activityLog;
    }
}
