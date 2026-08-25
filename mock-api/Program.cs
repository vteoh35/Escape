// Mock API for frontend development.
//
// Serves realistic fake data at the exact same routes documented in the real backend's
// backend/src/API/*/**.cs TODO comments and backend/src/API/program.cs checklist. All data is
// in-memory and resets whenever this process restarts -- nothing here is persisted, this is not
// a database.
//
// When the real backend endpoints are ready, point the frontend's API base URL at the real
// backend instead of this project -- no other frontend code changes should be needed, since the
// routes, request shapes, and response shapes here match what the real backend TODOs specify.
//
// Run with: dotnet run --project mock-api
// Default URL: see mock-api/Properties/launchSettings.json, or pass --urls http://localhost:5100

using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

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

var app = builder.Build();

app.UseCors("Frontend");

app.MapGet("/", () => "Mock API is running");

// ===== Seed data =====

var tasks = new List<TaskItem>
{
    new("TSK001", "Design homepage mockup", "Create Figma mockup for homepage", 2, "2026-01-05", "2026-01-15", 3, "PRJ001", null),
    new("TSK002", "Implement homepage", "Build homepage in React", 3, "2026-01-16", "2026-02-01", 2, "PRJ001", "TSK001"),
    new("TSK003", "Set up CI pipeline", "Configure GitHub Actions for build/test", 2, "2026-01-10", "2026-01-12", 1, "PRJ002", null),
};

var projects = new List<Project>
{
    new("PRJ001", "Website Redesign", "Refresh the marketing site", 3, 2, "2026-01-01", "2026-03-01"),
    new("PRJ002", "Internal Tooling", "Developer productivity tools", 2, 1, "2026-01-05", "2026-06-01"),
};

var employees = new List<Employee>
{
    new("EMP001", "Alice Johnson", "alice@company.com", 3, "Management", null),
    new("EMP002", "Bob Smith", "bob@company.com", 2, "Engineering", null),
    new("EMP003", "Charlie Lee", "charlie@company.com", 1, "Engineering", null),
};

var comments = new List<Comment>
{
    new("CMT001", "Looks good, ship it!", "TSK001", "EMP001", "2026-01-14T10:00:00Z", null),
    new("CMT002", "Can we use the new color palette?", "TSK002", "EMP002", "2026-01-20T09:30:00Z", null),
};

var attachments = new List<Attachment>
{
    new("ATT001", "/files/homepage_mockup.png", "PRJ001", "TSK001"),
};

var activityLogs = new List<ActivityLog>
{
    new("LOG001", "Task status changed to In Progress", null, "TSK002", "EMP002", "2026-01-16T08:00:00Z"),
};

var tags = new List<Tag> { new(1, "urgent"), new(2, "design"), new(3, "backend") };
var taskTags = new List<TaskTag> { new("TSK001", 2) };
var projectTags = new List<ProjectTag> { new("PRJ001", 2) };

var roles = new List<Role> { new(1, "Admin"), new(2, "Manager"), new(3, "Developer") };
var permissions = new List<Permission> { new(1, "manage_users"), new(2, "edit_project"), new(3, "delete_task") };
var rolePermissions = new List<RolePermission> { new(1, 1), new(1, 2), new(1, 3), new(2, 2) };

var priorities = new List<Priority> { new(1, "Low"), new(2, "Medium"), new(3, "High") };
var statuses = new List<Status> { new(1, "To Do"), new(2, "In Progress"), new(3, "Completed") };
var positionLevels = new List<PositionLevel> { new(1, "Junior Developer"), new(2, "Senior Developer"), new(3, "Project Manager") };

var projectMembers = new List<ProjectMember> { new("EMP001", "PRJ001", "Project Manager"), new("EMP002", "PRJ001", "Developer") };
var taskAssignees = new List<TaskAssignee> { new("EMP003", "TSK001", "Designer"), new("EMP002", "TSK002", "Developer") };

int nextTagId = tags.Count + 1;
int nextRoleId = roles.Count + 1;
int nextPermissionId = permissions.Count + 1;

// ===== Tasks =====

app.MapGet("/tasks", () => tasks);
app.MapGet("/tasks/{id}", (string id) => tasks.FirstOrDefault(t => t.TaskId == id) is { } t ? Results.Ok(t) : Results.NotFound());
app.MapPost("/tasks", ([FromBody] TaskItem task) => { tasks.Add(task); return Results.Created($"/tasks/{task.TaskId}", task); });
app.MapPut("/tasks/{id}", (string id, [FromBody] TaskItem updated) =>
{
    var index = tasks.FindIndex(t => t.TaskId == id);
    if (index < 0) return Results.NotFound();
    tasks[index] = updated with { TaskId = id };
    return Results.Ok(tasks[index]);
});
app.MapDelete("/tasks/{id}", (string id) => tasks.RemoveAll(t => t.TaskId == id) > 0 ? Results.NoContent() : Results.NotFound());

app.MapGet("/tasks/{id}/assignees", (string id) => taskAssignees.Where(a => a.TaskId == id));
app.MapPost("/tasks/{id}/assignees", (string id, [FromBody] TaskAssignee body) => { taskAssignees.Add(body with { TaskId = id }); return Results.Created($"/tasks/{id}/assignees", body); });
app.MapPut("/tasks/{id}/assignees/{employeeId}", (string id, string employeeId, [FromBody] TaskAssignee body) =>
{
    var index = taskAssignees.FindIndex(a => a.TaskId == id && a.EmployeeId == employeeId);
    if (index < 0) return Results.NotFound();
    taskAssignees[index] = taskAssignees[index] with { Role = body.Role };
    return Results.Ok(taskAssignees[index]);
});
app.MapDelete("/tasks/{id}/assignees/{employeeId}", (string id, string employeeId) =>
    taskAssignees.RemoveAll(a => a.TaskId == id && a.EmployeeId == employeeId) > 0 ? Results.NoContent() : Results.NotFound());

app.MapGet("/tasks/{id}/tags", (string id) => taskTags.Where(tt => tt.TaskId == id).Select(tt => tags.FirstOrDefault(t => t.TagId == tt.TagId)));
app.MapPost("/tasks/{id}/tags/{tagId}", (string id, int tagId) => { taskTags.Add(new TaskTag(id, tagId)); return Results.Created($"/tasks/{id}/tags/{tagId}", null); });
app.MapDelete("/tasks/{id}/tags/{tagId}", (string id, int tagId) =>
    taskTags.RemoveAll(tt => tt.TaskId == id && tt.TagId == tagId) > 0 ? Results.NoContent() : Results.NotFound());

// ===== Projects =====

app.MapGet("/projects", () => projects);
app.MapGet("/projects/{id}", (string id) => projects.FirstOrDefault(p => p.ProjectID == id) is { } p ? Results.Ok(p) : Results.NotFound());
app.MapPost("/projects", ([FromBody] Project project) => { projects.Add(project); return Results.Created($"/projects/{project.ProjectID}", project); });
app.MapPut("/projects/{id}", (string id, [FromBody] Project updated) =>
{
    var index = projects.FindIndex(p => p.ProjectID == id);
    if (index < 0) return Results.NotFound();
    projects[index] = updated with { ProjectID = id };
    return Results.Ok(projects[index]);
});
app.MapDelete("/projects/{id}", (string id) => projects.RemoveAll(p => p.ProjectID == id) > 0 ? Results.NoContent() : Results.NotFound());

app.MapGet("/projects/{id}/members", (string id) => projectMembers.Where(m => m.ProjectId == id));
app.MapPost("/projects/{id}/members", (string id, [FromBody] ProjectMember body) => { projectMembers.Add(body with { ProjectId = id }); return Results.Created($"/projects/{id}/members", body); });
app.MapPut("/projects/{id}/members/{employeeId}", (string id, string employeeId, [FromBody] ProjectMember body) =>
{
    var index = projectMembers.FindIndex(m => m.ProjectId == id && m.EmployeeId == employeeId);
    if (index < 0) return Results.NotFound();
    projectMembers[index] = projectMembers[index] with { Role = body.Role };
    return Results.Ok(projectMembers[index]);
});
app.MapDelete("/projects/{id}/members/{employeeId}", (string id, string employeeId) =>
    projectMembers.RemoveAll(m => m.ProjectId == id && m.EmployeeId == employeeId) > 0 ? Results.NoContent() : Results.NotFound());

app.MapGet("/projects/{id}/tags", (string id) => projectTags.Where(pt => pt.ProjectId == id).Select(pt => tags.FirstOrDefault(t => t.TagId == pt.TagId)));
app.MapPost("/projects/{id}/tags/{tagId}", (string id, int tagId) => { projectTags.Add(new ProjectTag(id, tagId)); return Results.Created($"/projects/{id}/tags/{tagId}", null); });
app.MapDelete("/projects/{id}/tags/{tagId}", (string id, int tagId) =>
    projectTags.RemoveAll(pt => pt.ProjectId == id && pt.TagId == tagId) > 0 ? Results.NoContent() : Results.NotFound());

// ===== Employees =====

app.MapGet("/employees", () => employees);
app.MapGet("/employees/{id}", (string id) => employees.FirstOrDefault(e => e.EmployeeId == id) is { } e ? Results.Ok(e) : Results.NotFound());
app.MapPost("/employees", ([FromBody] Employee employee) => { employees.Add(employee); return Results.Created($"/employees/{employee.EmployeeId}", employee); });
app.MapPut("/employees/{id}", (string id, [FromBody] Employee updated) =>
{
    var index = employees.FindIndex(e => e.EmployeeId == id);
    if (index < 0) return Results.NotFound();
    employees[index] = updated with { EmployeeId = id };
    return Results.Ok(employees[index]);
});
app.MapDelete("/employees/{id}", (string id) => employees.RemoveAll(e => e.EmployeeId == id) > 0 ? Results.NoContent() : Results.NotFound());

// ===== Comments =====

app.MapGet("/comments", () => comments);
app.MapGet("/comments/{id}", (string id) => comments.FirstOrDefault(c => c.CommentId == id) is { } c ? Results.Ok(c) : Results.NotFound());
app.MapPost("/comments", ([FromBody] Comment comment) => { comments.Add(comment); return Results.Created($"/comments/{comment.CommentId}", comment); });
app.MapPut("/comments/{id}", (string id, [FromBody] Comment updated) =>
{
    var index = comments.FindIndex(c => c.CommentId == id);
    if (index < 0) return Results.NotFound();
    comments[index] = updated with { CommentId = id };
    return Results.Ok(comments[index]);
});
app.MapDelete("/comments/{id}", (string id) => comments.RemoveAll(c => c.CommentId == id) > 0 ? Results.NoContent() : Results.NotFound());

// ===== Attachments =====

app.MapGet("/attachments", () => attachments);
app.MapGet("/attachments/{id}", (string id) => attachments.FirstOrDefault(a => a.AttachmentId == id) is { } a ? Results.Ok(a) : Results.NotFound());
app.MapPost("/attachments", ([FromBody] Attachment attachment) => { attachments.Add(attachment); return Results.Created($"/attachments/{attachment.AttachmentId}", attachment); });
app.MapPut("/attachments/{id}", (string id, [FromBody] Attachment updated) =>
{
    var index = attachments.FindIndex(a => a.AttachmentId == id);
    if (index < 0) return Results.NotFound();
    attachments[index] = updated with { AttachmentId = id };
    return Results.Ok(attachments[index]);
});
app.MapDelete("/attachments/{id}", (string id) => attachments.RemoveAll(a => a.AttachmentId == id) > 0 ? Results.NoContent() : Results.NotFound());

// ===== Tags =====

app.MapGet("/tags", () => tags);
app.MapGet("/tags/{id}", (int id) => tags.FirstOrDefault(t => t.TagId == id) is { } t ? Results.Ok(t) : Results.NotFound());
app.MapPost("/tags", ([FromBody] TagCreateRequest body) => { var tag = new Tag(nextTagId++, body.TagName); tags.Add(tag); return Results.Created($"/tags/{tag.TagId}", tag); });
app.MapPut("/tags/{id}", (int id, [FromBody] TagCreateRequest body) =>
{
    var index = tags.FindIndex(t => t.TagId == id);
    if (index < 0) return Results.NotFound();
    tags[index] = tags[index] with { TagName = body.TagName };
    return Results.Ok(tags[index]);
});
app.MapDelete("/tags/{id}", (int id) => tags.RemoveAll(t => t.TagId == id) > 0 ? Results.NoContent() : Results.NotFound());

// ===== Activity logs =====

app.MapGet("/activity-logs", () => activityLogs);
app.MapGet("/activity-logs/{id}", (string id) => activityLogs.FirstOrDefault(l => l.LogId == id) is { } l ? Results.Ok(l) : Results.NotFound());
app.MapPost("/activity-logs", ([FromBody] ActivityLog log) => { activityLogs.Add(log); return Results.Created($"/activity-logs/{log.LogId}", log); });
app.MapPut("/activity-logs/{id}", (string id, [FromBody] ActivityLog updated) =>
{
    var index = activityLogs.FindIndex(l => l.LogId == id);
    if (index < 0) return Results.NotFound();
    activityLogs[index] = updated with { LogId = id };
    return Results.Ok(activityLogs[index]);
});
app.MapDelete("/activity-logs/{id}", (string id) => activityLogs.RemoveAll(l => l.LogId == id) > 0 ? Results.NoContent() : Results.NotFound());

// ===== Roles + Permissions =====

app.MapGet("/roles", () => roles);
app.MapGet("/roles/{id}", (int id) => roles.FirstOrDefault(r => r.RoleId == id) is { } r ? Results.Ok(r) : Results.NotFound());
app.MapPost("/roles", ([FromBody] RoleCreateRequest body) => { var role = new Role(nextRoleId++, body.RoleName); roles.Add(role); return Results.Created($"/roles/{role.RoleId}", role); });
app.MapPut("/roles/{id}", (int id, [FromBody] RoleCreateRequest body) =>
{
    var index = roles.FindIndex(r => r.RoleId == id);
    if (index < 0) return Results.NotFound();
    roles[index] = roles[index] with { RoleName = body.RoleName };
    return Results.Ok(roles[index]);
});
app.MapDelete("/roles/{id}", (int id) => roles.RemoveAll(r => r.RoleId == id) > 0 ? Results.NoContent() : Results.NotFound());

app.MapGet("/roles/{id}/permissions", (int id) => rolePermissions.Where(rp => rp.RoleId == id).Select(rp => permissions.FirstOrDefault(p => p.PermissionId == rp.PermissionId)));
app.MapPost("/roles/{id}/permissions/{permissionId}", (int id, int permissionId) => { rolePermissions.Add(new RolePermission(id, permissionId)); return Results.Created($"/roles/{id}/permissions/{permissionId}", null); });
app.MapDelete("/roles/{id}/permissions/{permissionId}", (int id, int permissionId) =>
    rolePermissions.RemoveAll(rp => rp.RoleId == id && rp.PermissionId == permissionId) > 0 ? Results.NoContent() : Results.NotFound());

app.MapGet("/permissions", () => permissions);
app.MapGet("/permissions/{id}", (int id) => permissions.FirstOrDefault(p => p.PermissionId == id) is { } p ? Results.Ok(p) : Results.NotFound());
app.MapPost("/permissions", ([FromBody] PermissionCreateRequest body) => { var permission = new Permission(nextPermissionId++, body.PermissionName); permissions.Add(permission); return Results.Created($"/permissions/{permission.PermissionId}", permission); });
app.MapPut("/permissions/{id}", (int id, [FromBody] PermissionCreateRequest body) =>
{
    var index = permissions.FindIndex(p => p.PermissionId == id);
    if (index < 0) return Results.NotFound();
    permissions[index] = permissions[index] with { PermissionName = body.PermissionName };
    return Results.Ok(permissions[index]);
});
app.MapDelete("/permissions/{id}", (int id) => permissions.RemoveAll(p => p.PermissionId == id) > 0 ? Results.NoContent() : Results.NotFound());

// ===== Priorities / Statuses / PositionLevels (small lookup tables) =====

app.MapGet("/priorities", () => priorities);
app.MapGet("/priorities/{id}", (int id) => priorities.FirstOrDefault(p => p.PriorityId == id) is { } p ? Results.Ok(p) : Results.NotFound());
app.MapPost("/priorities", ([FromBody] Priority priority) => { priorities.Add(priority); return Results.Created($"/priorities/{priority.PriorityId}", priority); });
app.MapPut("/priorities/{id}", (int id, [FromBody] Priority updated) =>
{
    var index = priorities.FindIndex(p => p.PriorityId == id);
    if (index < 0) return Results.NotFound();
    priorities[index] = updated with { PriorityId = id };
    return Results.Ok(priorities[index]);
});
app.MapDelete("/priorities/{id}", (int id) => priorities.RemoveAll(p => p.PriorityId == id) > 0 ? Results.NoContent() : Results.NotFound());

app.MapGet("/statuses", () => statuses);
app.MapGet("/statuses/{id}", (int id) => statuses.FirstOrDefault(s => s.StatusId == id) is { } s ? Results.Ok(s) : Results.NotFound());
app.MapPost("/statuses", ([FromBody] Status status) => { statuses.Add(status); return Results.Created($"/statuses/{status.StatusId}", status); });
app.MapPut("/statuses/{id}", (int id, [FromBody] Status updated) =>
{
    var index = statuses.FindIndex(s => s.StatusId == id);
    if (index < 0) return Results.NotFound();
    statuses[index] = updated with { StatusId = id };
    return Results.Ok(statuses[index]);
});
app.MapDelete("/statuses/{id}", (int id) => statuses.RemoveAll(s => s.StatusId == id) > 0 ? Results.NoContent() : Results.NotFound());

app.MapGet("/position-levels", () => positionLevels);
app.MapGet("/position-levels/{level}", (int level) => positionLevels.FirstOrDefault(p => p.Level == level) is { } p ? Results.Ok(p) : Results.NotFound());
app.MapPost("/position-levels", ([FromBody] PositionLevel positionLevel) => { positionLevels.Add(positionLevel); return Results.Created($"/position-levels/{positionLevel.Level}", positionLevel); });
app.MapPut("/position-levels/{level}", (int level, [FromBody] PositionLevel updated) =>
{
    var index = positionLevels.FindIndex(p => p.Level == level);
    if (index < 0) return Results.NotFound();
    positionLevels[index] = updated with { Level = level };
    return Results.Ok(positionLevels[index]);
});
app.MapDelete("/position-levels/{level}", (int level) => positionLevels.RemoveAll(p => p.Level == level) > 0 ? Results.NoContent() : Results.NotFound());

// ===== Auth (mock -- accepts anything, always "succeeds") =====
//
// This does NOT validate credentials against anything real -- it's here so the frontend can build
// its login flow and receive a token-shaped string to attach as a Bearer header, without needing a
// real account system yet. Any employeeId/password combination "succeeds".

app.MapPost("/auth/login", ([FromBody] LoginRequest body) =>
    Results.Ok(new { token = $"mock-token-for-{body.EmployeeId}" }));

app.MapPost("/auth/register", ([FromBody] LoginRequest body) =>
    Results.Ok(new { employeeId = body.EmployeeId, registered = true }));

app.Run();

// ===== Data shapes (mirror the real Business_Logic entities) =====

record TaskItem(string TaskId, string Name, string? Description, int? PriorityId, string? StartDate, string? EndDate, int? StatusId, string? ProjectId, string? ParentTaskId);
record Project(string ProjectID, string Name, string? Description, int? PriorityId, int? StatusId, string? StartDate, string? EndDate);
record Employee(string EmployeeId, string Name, string Email, int? EmployeeLevel, string? Department, int? RoleId);
record Comment(string CommentId, string? Description, string? TaskId, string? EmployeeId, string? CommentTime, string? ParentCommentId);
record Attachment(string AttachmentId, string AttachmentLocation, string? ProjectId, string? TaskId);
record ActivityLog(string LogId, string? Description, string? ProjectId, string? TaskId, string? EmployeeId, string? LogTime);
record Tag(int TagId, string TagName);
record TaskTag(string TaskId, int TagId);
record ProjectTag(string ProjectId, int TagId);
record Role(int RoleId, string RoleName);
record Permission(int PermissionId, string PermissionName);
record RolePermission(int RoleId, int PermissionId);
record Priority(int PriorityId, string PriorityName);
record Status(int StatusId, string StatusName);
record PositionLevel(int Level, string? Position);
record ProjectMember(string EmployeeId, string ProjectId, string? Role);
record TaskAssignee(string EmployeeId, string TaskId, string? Role);

record TagCreateRequest(string TagName);
record RoleCreateRequest(string RoleName);
record PermissionCreateRequest(string PermissionName);
record LoginRequest(string EmployeeId, string Password);
