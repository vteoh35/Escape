using Application.Priorities;

namespace API.Priorities;

public static class PrioritiesAPI
{
    public static void MapPriorityEndpoints(this WebApplication app)
    {
        app.MapGet("/priorities", (GetPriority getPriority) => Results.Ok(getPriority.GetAll()));

        app.MapGet("/priorities/{id}", (int id, GetPriority getPriority) =>
        {
            var priority = getPriority.GetById(id);
            return priority == null ? Results.NotFound() : Results.Ok(priority);
        });

        app.MapPost("/priorities", (PriorityRequest request, CreatePriority createPriority) =>
        {
            var priority = createPriority.Execute(request.PriorityId, request.PriorityName);
            return Results.Created($"/priorities/{priority.PriorityId}", priority);
        });

        app.MapPut("/priorities/{id}", (int id, PriorityRequest request, UpdatePriority updatePriority) =>
        {
            var priority = updatePriority.Execute(id, request.PriorityName);
            return priority == null ? Results.NotFound() : Results.Ok(priority);
        });

        app.MapDelete("/priorities/{id}", (int id, DeletePriority deletePriority) =>
        {
            var deleted = deletePriority.Execute(id);
            return deleted ? Results.NoContent() : Results.NotFound();
        });
    }
}

public record PriorityRequest(int PriorityId, string PriorityName);
