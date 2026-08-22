using Application.Tasks;

namespace API.Tasks;

public static class TasksAPI
{
    public static void MapTaskEndpoints(this WebApplication app)
    {
        app.MapPost("/tasks", (CreateTaskRequest request, CreateTask createTask) =>
        {
            var task = createTask.Execute(request.Name);

            return Results.Created($"/tasks/{task.Id}", task);
        });
    }
}

public record CreateTaskRequest(string Name);