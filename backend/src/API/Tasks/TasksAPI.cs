using Application.Tasks;

namespace API.Tasks;

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
    }
}

public record CreateTaskRequest(string TaskId, string Name);

public record UpdateTaskRequest(string Name);