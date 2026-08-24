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
        app.MapGet("/tasks/{id}", (int id, GetTask getTask) =>
        {
            var task = getTask.GetById(id);

            if (task == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(task);
        });

        // Create a task
        app.MapPost("/tasks", (CreateTaskRequest request, CreateTask createTask) =>
        {
            var task = createTask.Execute(request.Name);

            return Results.Created($"/tasks/{task.Id}", task);
        });

        // Update a task
        app.MapPut("/tasks/{id}", (
            int id,
            UpdateTaskRequest request,
            UpdateTask updateTask) =>
        {
            var task = updateTask.Execute(
                id,
                request.Name,
                request.IsCompleted
            );

            if (task == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(task);
        });

        // Delete a task
        app.MapDelete("/tasks/{id}", (int id, DeleteTask deleteTask) =>
        {
            var deleted = deleteTask.Execute(id);

            if (!deleted)
            {
                return Results.NotFound();
            }

            return Results.NoContent();
        });
    }
}

public record CreateTaskRequest(string Name);

public record UpdateTaskRequest(
    string Name,
    bool IsCompleted
);