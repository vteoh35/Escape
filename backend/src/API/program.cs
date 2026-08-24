using API.Tasks;
using Application.Tasks;
using Infrastructure.Tasks;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<CreateTask>();

var app = builder.Build();

app.MapGet("/", () => "Escape API is running");

app.MapTaskEndpoints();

app.Run();