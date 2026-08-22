using API.Tasks;
using Application.Tasks;
using Infrastructure.Tasks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<CreateTask>();

var app = builder.Build();

app.MapGet("/", () => "Escape API is running");

app.MapTaskEndpoints();

app.Run();