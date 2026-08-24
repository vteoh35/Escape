using API.Tasks;
using Application.Tasks;
using Infrastructure.Tasks;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

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

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ITaskRepository, TaskRepository>();

builder.Services.AddScoped<CreateTask>();
builder.Services.AddScoped<GetTask>();
builder.Services.AddScoped<UpdateTask>();
builder.Services.AddScoped<DeleteTask>();

var app = builder.Build();

app.UseCors("Frontend");

app.MapGet("/", () => "Escape API is running");

app.MapTaskEndpoints();

// TODO: as each *API.cs file above gets implemented, register its DI services above (repositories
// as AddScoped, they all take AppDbContext; use case classes as AddScoped too) and call its
// Map*Endpoints() extension method here, same as MapTaskEndpoints(). Files still needing this:
//   API/Projects/ProjectsAPI.cs
//   API/Employees/EmployeesAPI.cs
//   API/Comments/CommentsAPI.cs
//   API/Attachments/AttachmentsAPI.cs
//   API/Tags/TagsAPI.cs
//   API/Authentication/AuthenticationAPI.cs
//   API/ActivityLogs/ActivityLogsAPI.cs
//   API/Roles/RolesAPI.cs
//   API/Permissions/PermissionsAPI.cs
//   API/Priorities/PrioritiesAPI.cs
//   API/Statuses/StatusesAPI.cs
//   API/PositionLevels/PositionLevelsAPI.cs
//   API/Tasks/TasksAPI.cs (assignees + tags -- see TODO at the bottom of that file)
//
// TODO: wire up middleware (see TODOs in each file for details):
//   app.UseMiddleware<ExceptionMiddleware>()   -- add early, before routing
//   app.UseMiddleware<LoggingMiddleware>()
//   builder.Services.AddAuthentication(...).AddJwtBearer(...) + app.UseAuthentication() +
//     app.UseAuthorization()  -- see API/middleware/AuthenticatonMiddleware.cs

app.Run();