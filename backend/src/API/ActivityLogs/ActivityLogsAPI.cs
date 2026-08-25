using Application.ActivityLogs;

namespace API.ActivityLogs;

public static class ActivityLogsAPI
{
    public static void MapActivityLogEndpoints(this WebApplication app)
    {
        app.MapGet("/activity-logs", (GetActivityLog getActivityLog) => Results.Ok(getActivityLog.GetAll()));

        app.MapGet("/activity-logs/{id}", (string id, GetActivityLog getActivityLog) =>
        {
            var log = getActivityLog.GetById(id);
            return log == null ? Results.NotFound() : Results.Ok(log);
        });

        app.MapPost("/activity-logs", (CreateActivityLogRequest request, CreateActivityLog createActivityLog) =>
        {
            var log = createActivityLog.Execute(request.LogId, request.Description, request.ProjectId, request.TaskId, request.EmployeeId);
            return Results.Created($"/activity-logs/{log.LogId}", log);
        });

        app.MapPut("/activity-logs/{id}", (string id, UpdateActivityLogRequest request, UpdateActivityLog updateActivityLog) =>
        {
            var log = updateActivityLog.Execute(id, request.Description);
            return log == null ? Results.NotFound() : Results.Ok(log);
        });

        app.MapDelete("/activity-logs/{id}", (string id, DeleteActivityLog deleteActivityLog) =>
        {
            var deleted = deleteActivityLog.Execute(id);
            return deleted ? Results.NoContent() : Results.NotFound();
        });
    }
}

public record CreateActivityLogRequest(string LogId, string Description, string? ProjectId, string? TaskId, string? EmployeeId);
public record UpdateActivityLogRequest(string Description);
