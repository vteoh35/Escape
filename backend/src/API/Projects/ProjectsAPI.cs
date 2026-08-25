using Application.Projects;
using Application.Tags;

namespace API.Projects;

public static class ProjectsAPI
{
    public static void MapProjectEndpoints(this WebApplication app)
    {
        app.MapGet("/projects", (GetProject getProject) => Results.Ok(getProject.GetAll()));

        app.MapGet("/projects/{id}", (string id, GetProject getProject) =>
        {
            var project = getProject.GetById(id);
            return project == null ? Results.NotFound() : Results.Ok(project);
        });

        app.MapPost("/projects", (CreateProjectRequest request, CreateProject createProject) =>
        {
            var project = createProject.Execute(request.ProjectId, request.Name);
            return Results.Created($"/projects/{project.ProjectID}", project);
        });

        app.MapPut("/projects/{id}", (string id, UpdateProjectRequest request, UpdateProject updateProject) =>
        {
            var project = updateProject.Execute(id, request.Name);
            return project == null ? Results.NotFound() : Results.Ok(project);
        });

        app.MapDelete("/projects/{id}", (string id, DeleteProject deleteProject) =>
        {
            var deleted = deleteProject.Execute(id);
            return deleted ? Results.NoContent() : Results.NotFound();
        });

        app.MapGet("/projects/{id}/members", (string id, GetProjectMembers getProjectMembers) =>
            Results.Ok(getProjectMembers.Execute(id)));

        app.MapPost("/projects/{id}/members", (string id, AddProjectMemberRequest request, AddProjectMember addProjectMember) =>
        {
            var member = addProjectMember.Execute(request.EmployeeId, id, request.Role);
            return Results.Created($"/projects/{id}/members/{request.EmployeeId}", member);
        });

        app.MapPut("/projects/{id}/members/{employeeId}", (string id, string employeeId, UpdateProjectMemberRoleRequest request, UpdateProjectMemberRole updateRole) =>
        {
            var member = updateRole.Execute(employeeId, id, request.Role);
            return member == null ? Results.NotFound() : Results.Ok(member);
        });

        app.MapDelete("/projects/{id}/members/{employeeId}", (string id, string employeeId, RemoveProjectMember removeMember) =>
        {
            var removed = removeMember.Execute(employeeId, id);
            return removed ? Results.NoContent() : Results.NotFound();
        });

        app.MapGet("/projects/{id}/tags", (string id, GetProjectTags getProjectTags) =>
            Results.Ok(getProjectTags.Execute(id)));

        app.MapPost("/projects/{id}/tags/{tagId}", (string id, int tagId, TagProject tagProject) =>
        {
            var projectTag = tagProject.Execute(id, tagId);
            return Results.Created($"/projects/{id}/tags/{tagId}", projectTag);
        });

        app.MapDelete("/projects/{id}/tags/{tagId}", (string id, int tagId, UntagProject untagProject) =>
        {
            var removed = untagProject.Execute(id, tagId);
            return removed ? Results.NoContent() : Results.NotFound();
        });
    }
}

public record CreateProjectRequest(string ProjectId, string Name);
public record UpdateProjectRequest(string Name);
public record AddProjectMemberRequest(string EmployeeId, string? Role);
public record UpdateProjectMemberRoleRequest(string? Role);
