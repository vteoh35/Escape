using API.Tasks;
using Application.Tasks;
using Infrastructure.Tasks;

var builder = WebApplication.CreateBuilder(args);

// Allow Angular frontend to call this API
builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddSingleton<ITaskRepository, TaskRepository>();

builder.Services.AddScoped<CreateTask>();
builder.Services.AddScoped<GetTask>();
builder.Services.AddScoped<UpdateTask>();
builder.Services.AddScoped<DeleteTask>();

var app = builder.Build();

app.UseCors("Frontend");

app.MapGet("/", () => "Escape API is running");

app.MapTaskEndpoints();

app.Run();