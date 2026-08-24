namespace Business_Logic.ActivityLogs;

public class ActivityLog
{
    public string LogId { get; set; }
    public string? Description { get; set; }
    public string? ProjectId { get; set; }
    public string? TaskId { get; set; }
    public string? EmployeeId { get; set; }
    public DateTime? LogTime { get; set; }
}