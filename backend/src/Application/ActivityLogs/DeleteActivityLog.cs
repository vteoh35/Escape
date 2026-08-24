namespace Application.ActivityLogs;

public class DeleteActivityLog
{
    private readonly IActivityLogRepository _activityLogRepository;

    public DeleteActivityLog(IActivityLogRepository activityLogRepository)
    {
        _activityLogRepository = activityLogRepository;
    }

    public bool Execute(string logId)
    {
        var activityLog = _activityLogRepository.GetById(logId);

        if (activityLog == null)
        {
            return false;
        }

        _activityLogRepository.Delete(activityLog);

        return true;
    }
}
