var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

var tasks = new List<TaskItem>();

app.MapGet("/tasks", () =>
{
    return tasks;
});

app.MapPost("/tasks", (TaskItem task) =>
{
    tasks.Add(task);

    return Results.Ok(task);
});

app.MapGet("/tasks/{id}", (int id) =>
{
    var task = tasks.FirstOrDefault(t => t.Id == id);

    return task is not null
        ? Results.Ok(task)
        : Results.NotFound();
});

app.MapDelete("/tasks/{id}", (int id) =>
{
    var task = tasks.FirstOrDefault(t => t.Id == id);

    if (task is null)
    {
        return Results.NotFound();
    }

    tasks.Remove(task);

    return Results.NoContent();
});

app.MapPut("/tasks/{id}", (int id, TaskItem updatedTask) =>
{
    var index = tasks.FindIndex(t => t.Id == id);

    if (index == -1)
    {
        return Results.NotFound();
    }

    tasks[index] = updatedTask;

    return Results.Ok(updatedTask);
});

app.Run();

record TaskItem(int Id, string Name);