using System.Text;
using API.Middleware;
using API.Tasks;
using Application.Authentication;
using Application.Authorization;
using Application.Employees;
using Application.Permissions;
using Application.Roles;
using Application.Tasks;
using Infrastructure.Authentication;
using Infrastructure.Authorization;
using Infrastructure.Employees;
using Infrastructure.Tasks;
using Infrastructure.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

// Authentication (JWT) and authorization (permission-based)
var jwtKey = builder.Configuration["Jwt:Key"]
    ?? throw new InvalidOperationException("Missing configuration value: Jwt:Key");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<GetEmployeePermissions>();

builder.Services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<ITokenService>(_ => new TokenService(jwtKey));
builder.Services.AddScoped<RegisterCredentials>();
builder.Services.AddScoped<Login>();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors("Frontend");

app.UseAuthentication();
app.UseAuthorization();

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
// TODO: wire up remaining middleware (see TODO in the file for details):
//   app.UseMiddleware<LoggingMiddleware>()
//
// Exception handling is already wired above (ExceptionMiddleware, first in the pipeline) -- any
// unhandled exception anywhere downstream (including in auth/EF/your endpoints) becomes a generic
// 500 JSON response instead of leaking a stack trace. It currently returns the same generic message
// for every exception type; see the TODO in ExceptionMiddleware.cs if you want specific exception
// types mapped to specific status codes later (e.g. a "not found" domain exception -> 404).
//
// Authentication/authorization is already wired above. To require a valid token on a route:
//   app.MapPost("/projects", ...).RequireAuthorization();
// To require a specific permission (checked via GetEmployeePermissions against the caller's
// Employee.RoleId -> RolePermissions -> Permission.PermissionName):
//   app.MapDelete("/projects/{id}", ...)
//       .RequireAuthorization(policy => policy.Requirements.Add(new PermissionRequirement("delete_project")));
//   (PermissionRequirement is in Infrastructure.Authorization -- add a `using` for it.)
// The permission name is just whatever string you created via CreatePermission -- there's no
// fixed enum of permissions, they're rows in the permission table.

app.Run();