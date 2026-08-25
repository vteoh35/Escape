using System.Text;
using API.ActivityLogs;
using API.Attachments;
using API.Authentication;
using API.Comments;
using API.Employees;
using API.Middleware;
using API.Permissions;
using API.PositionLevels;
using API.Priorities;
using API.Projects;
using API.Roles;
using API.Statuses;
using API.Tags;
using API.Tasks;
using Application.ActivityLogs;
using Application.Attachments;
using Application.Authentication;
using Application.Authorization;
using Application.Comments;
using Application.Employees;
using Application.Permissions;
using Application.PositionLevels;
using Application.Priorities;
using Application.Projects;
using Application.Roles;
using Application.Statuses;
using Application.Tags;
using Application.Tasks;
using Infrastructure.ActivityLogs;
using Infrastructure.Attachments;
using Infrastructure.Authentication;
using Infrastructure.Authorization;
using Infrastructure.Comments;
using Infrastructure.Database;
using Infrastructure.Employees;
using Infrastructure.Priorities;
using Infrastructure.Projects;
using Infrastructure.Statuses;
using Infrastructure.Tags;
using Infrastructure.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Allow the Angular frontend to call this API. localhost:4200 always works for local dev;
// the deployed frontend origin (e.g. a Netlify URL) is added via the Cors:AdditionalOrigins
// config value (comma-separated) so it doesn't need a code change/redeploy to update.
var allowedOrigins = new List<string> { "http://localhost:4200" };
var additionalOrigins = builder.Configuration["Cors:AdditionalOrigins"];
if (!string.IsNullOrWhiteSpace(additionalOrigins))
{
    allowedOrigins.AddRange(additionalOrigins.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries));
}

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy
            .WithOrigins(allowedOrigins.ToArray())
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Tasks
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskAssigneeRepository, TaskAssigneeRepository>();
builder.Services.AddScoped<CreateTask>();
builder.Services.AddScoped<GetTask>();
builder.Services.AddScoped<UpdateTask>();
builder.Services.AddScoped<DeleteTask>();
builder.Services.AddScoped<AssignEmployeeToTask>();
builder.Services.AddScoped<UnassignEmployeeFromTask>();
builder.Services.AddScoped<GetTaskAssignees>();
builder.Services.AddScoped<UpdateTaskAssigneeRole>();

// Projects
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IProjectMemberRepository, ProjectMemberRepository>();
builder.Services.AddScoped<CreateProject>();
builder.Services.AddScoped<GetProject>();
builder.Services.AddScoped<UpdateProject>();
builder.Services.AddScoped<DeleteProject>();
builder.Services.AddScoped<AddProjectMember>();
builder.Services.AddScoped<RemoveProjectMember>();
builder.Services.AddScoped<GetProjectMembers>();
builder.Services.AddScoped<UpdateProjectMemberRole>();

// Comments
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<CreateComment>();
builder.Services.AddScoped<GetComment>();
builder.Services.AddScoped<UpdateComment>();
builder.Services.AddScoped<DeleteComment>();

// Attachments
builder.Services.AddScoped<IAttachmentRepository, AttachmentsRepository>();
builder.Services.AddScoped<CreateAttachment>();
builder.Services.AddScoped<GetAttachment>();
builder.Services.AddScoped<UpdateAttachment>();
builder.Services.AddScoped<DeleteAttachment>();

// Tags
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<ITaskTagRepository, TaskTagRepository>();
builder.Services.AddScoped<IProjectTagRepository, ProjectTagRepository>();
builder.Services.AddScoped<CreateTag>();
builder.Services.AddScoped<GetTag>();
builder.Services.AddScoped<UpdateTag>();
builder.Services.AddScoped<DeleteTag>();
builder.Services.AddScoped<TagTask>();
builder.Services.AddScoped<UntagTask>();
builder.Services.AddScoped<GetTaskTags>();
builder.Services.AddScoped<TagProject>();
builder.Services.AddScoped<UntagProject>();
builder.Services.AddScoped<GetProjectTags>();

// Activity logs
builder.Services.AddScoped<IActivityLogRepository, ActivityLogRepository>();
builder.Services.AddScoped<CreateActivityLog>();
builder.Services.AddScoped<GetActivityLog>();
builder.Services.AddScoped<UpdateActivityLog>();
builder.Services.AddScoped<DeleteActivityLog>();

// Priorities / Statuses / Position levels (lookup tables)
builder.Services.AddScoped<IPriorityRepository, PriorityRepository>();
builder.Services.AddScoped<CreatePriority>();
builder.Services.AddScoped<GetPriority>();
builder.Services.AddScoped<UpdatePriority>();
builder.Services.AddScoped<DeletePriority>();

builder.Services.AddScoped<IStatusRepository, StatusRepository>();
builder.Services.AddScoped<CreateStatus>();
builder.Services.AddScoped<GetStatus>();
builder.Services.AddScoped<UpdateStatus>();
builder.Services.AddScoped<DeleteStatus>();

builder.Services.AddScoped<IPositionLevelRepository, PositionLevelRepository>();
builder.Services.AddScoped<CreatePositionLevel>();
builder.Services.AddScoped<GetPositionLevel>();
builder.Services.AddScoped<UpdatePositionLevel>();
builder.Services.AddScoped<DeletePositionLevel>();

// Roles
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<CreateRole>();
builder.Services.AddScoped<GetRole>();
builder.Services.AddScoped<UpdateRole>();
builder.Services.AddScoped<DeleteRole>();
builder.Services.AddScoped<AssignPermissionToRole>();
builder.Services.AddScoped<RemovePermissionFromRole>();
builder.Services.AddScoped<GetRolePermissions>();

// Permissions
builder.Services.AddScoped<CreatePermission>();
builder.Services.AddScoped<GetPermission>();
builder.Services.AddScoped<UpdatePermission>();
builder.Services.AddScoped<DeletePermission>();

// Employees
builder.Services.AddScoped<CreateEmployee>();
builder.Services.AddScoped<GetEmployee>();
builder.Services.AddScoped<UpdateEmployee>();
builder.Services.AddScoped<DeleteEmployee>();

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
app.UseMiddleware<LoggingMiddleware>();

app.UseCors("Frontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "Escape API is running");

// TEMPORARY -- remove once CORS config is confirmed working.
app.MapGet("/debug/cors", (IConfiguration config) => new
{
    raw = config["Cors:AdditionalOrigins"],
    allowedOrigins
});

app.MapTaskEndpoints();
app.MapProjectEndpoints();
app.MapEmployeeEndpoints();
app.MapCommentEndpoints();
app.MapAttachmentEndpoints();
app.MapTagEndpoints();
app.MapAuthenticationEndpoints();
app.MapActivityLogEndpoints();
app.MapRoleEndpoints();
app.MapPermissionEndpoints();
app.MapPriorityEndpoints();
app.MapStatusEndpoints();
app.MapPositionLevelEndpoints();

// Every route above is unauthenticated by default. To require a valid token on a route:
//   app.MapPost("/projects", ...).RequireAuthorization();
// To require a specific permission (checked via GetEmployeePermissions against the caller's
// Employee.RoleId -> RolePermissions -> Permission.PermissionName):
//   app.MapDelete("/projects/{id}", ...)
//       .RequireAuthorization(policy => policy.Requirements.Add(new PermissionRequirement("delete_project")));
//   (PermissionRequirement is in Infrastructure.Authorization -- add a `using` for it.)
// The permission name is just whatever string you created via CreatePermission -- there's no
// fixed enum of permissions, they're rows in the permission table. Deciding which routes need
// which permissions is a product decision, not made here -- left open intentionally.

app.Run();
