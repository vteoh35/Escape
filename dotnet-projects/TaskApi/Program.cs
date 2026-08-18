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

app.Run();

record TaskItem(int Id, string Name);