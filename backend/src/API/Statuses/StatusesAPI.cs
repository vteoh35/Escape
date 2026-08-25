using Application.Statuses;

namespace API.Statuses;

public static class StatusesAPI
{
    public static void MapStatusEndpoints(this WebApplication app)
    {
        app.MapGet("/statuses", (GetStatus getStatus) => Results.Ok(getStatus.GetAll()));

        app.MapGet("/statuses/{id}", (int id, GetStatus getStatus) =>
        {
            var status = getStatus.GetById(id);
            return status == null ? Results.NotFound() : Results.Ok(status);
        });

        app.MapPost("/statuses", (StatusRequest request, CreateStatus createStatus) =>
        {
            var status = createStatus.Execute(request.StatusId, request.StatusName);
            return Results.Created($"/statuses/{status.StatusId}", status);
        }).RequireAuthorization();

        app.MapPut("/statuses/{id}", (int id, StatusRequest request, UpdateStatus updateStatus) =>
        {
            var status = updateStatus.Execute(id, request.StatusName);
            return status == null ? Results.NotFound() : Results.Ok(status);
        }).RequireAuthorization();

        app.MapDelete("/statuses/{id}", (int id, DeleteStatus deleteStatus) =>
        {
            var deleted = deleteStatus.Execute(id);
            return deleted ? Results.NoContent() : Results.NotFound();
        }).RequireAuthorization();
    }
}

public record StatusRequest(int StatusId, string StatusName);
