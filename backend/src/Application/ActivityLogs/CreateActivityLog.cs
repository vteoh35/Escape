using Business_Logic.ActivityLogs;

namespace Application.ActivityLogs;

public class CreateActivityLog
{
    private readonly IActivityLogRepository _activityLogRepository;

    public CreateActivityLog(IActivityLogRepository activityLogRepository)
    {
        _activityLogRepository = activityLogRepository;
    }

    public ActivityLog Execute(string logId, string description, string? projectId, string? taskId, string? employeeId)
    {
        var activityLog = new ActivityLog
        {
            LogId = logId,
            Description = description,
            ProjectId = projectId,
            TaskId = taskId,
            EmployeeId = employeeId,
            LogTime = DateTime.UtcNow
        };

        _activityLogRepository.Add(activityLog);

        return activityLog;
    }
}
