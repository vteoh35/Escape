using Application.Tasks;

namespace API.Tasks;

/// <summary>
/// Task endpoints: /tasks, plus task assignee and task tagging sub-resources.
/// </summary>
public static class TasksAPI
{
    public static void MapTaskEndpoints(this WebApplication app)
    {
        // Get all tasks
        app.MapGet("/tasks", (GetTask getTask) =>
        {
            return Results.Ok(getTask.GetAll());
        });

        // Get one task
        app.MapGet("/tasks/{taskId}", (string taskId, GetTask getTask) =>
        {
            var task = getTask.GetById(taskId);

            if (task == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(task);
        });

        // Create a task
        app.MapPost("/tasks", (CreateTaskRequest request, CreateTask createTask) =>
        {
            var task = createTask.Execute(request.TaskId, request.Name);

            return Results.Created($"/tasks/{task.TaskId}", task);
        });

        // Update a task
        app.MapPut("/tasks/{taskId}", (
            string taskId,
            UpdateTaskRequest request,
            UpdateTask updateTask) =>
        {
            var task = updateTask.Execute(taskId, request.Name);

            if (task == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(task);
        });

        // Delete a task
        app.MapDelete("/tasks/{taskId}", (string taskId, DeleteTask deleteTask) =>
        {
            var deleted = deleteTask.Execute(taskId);

            if (!deleted)
            {
                return Results.NotFound();
            }

            return Results.NoContent();
        });

        app.MapGet("/tasks/{id}/assignees", (string id, GetTaskAssignees getTaskAssignees) =>
            Results.Ok(getTaskAssignees.Execute(id)));

        app.MapPost("/tasks/{id}/assignees", (string id, AssignTaskRequest request, AssignEmployeeToTask assignEmployeeToTask) =>
        {
            var assignee = assignEmployeeToTask.Execute(request.EmployeeId, id, request.Role);
            return Results.Created($"/tasks/{id}/assignees/{request.EmployeeId}", assignee);
        });

        app.MapPut("/tasks/{id}/assignees/{employeeId}", (string id, string employeeId, UpdateTaskAssigneeRoleRequest request, UpdateTaskAssigneeRole updateRole) =>
        {
            var assignee = updateRole.Execute(employeeId, id, request.Role);
            return assignee == null ? Results.NotFound() : Results.Ok(assignee);
        });

        app.MapDelete("/tasks/{id}/assignees/{employeeId}", (string id, string employeeId, UnassignEmployeeFromTask unassign) =>
        {
            var removed = unassign.Execute(employeeId, id);
            return removed ? Results.NoContent() : Results.NotFound();
        });
    }
}

public record CreateTaskRequest(string TaskId, string Name);

public record UpdateTaskRequest(string Name);

public record AssignTaskRequest(string EmployeeId, string? Role);

public record UpdateTaskAssigneeRoleRequest(string? Role);