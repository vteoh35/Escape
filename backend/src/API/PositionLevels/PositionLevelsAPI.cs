using Application.PositionLevels;

namespace API.PositionLevels;

public static class PositionLevelsAPI
{
    public static void MapPositionLevelEndpoints(this WebApplication app)
    {
        app.MapGet("/position-levels", (GetPositionLevel getPositionLevel) => Results.Ok(getPositionLevel.GetAll()));

        app.MapGet("/position-levels/{level}", (int level, GetPositionLevel getPositionLevel) =>
        {
            var positionLevel = getPositionLevel.GetByLevel(level);
            return positionLevel == null ? Results.NotFound() : Results.Ok(positionLevel);
        });

        app.MapPost("/position-levels", (PositionLevelRequest request, CreatePositionLevel createPositionLevel) =>
        {
            var positionLevel = createPositionLevel.Execute(request.Level, request.Position);
            return Results.Created($"/position-levels/{positionLevel.Level}", positionLevel);
        }).RequireAuthorization();

        app.MapPut("/position-levels/{level}", (int level, PositionLevelRequest request, UpdatePositionLevel updatePositionLevel) =>
        {
            var positionLevel = updatePositionLevel.Execute(level, request.Position);
            return positionLevel == null ? Results.NotFound() : Results.Ok(positionLevel);
        }).RequireAuthorization();

        app.MapDelete("/position-levels/{level}", (int level, DeletePositionLevel deletePositionLevel) =>
        {
            var deleted = deletePositionLevel.Execute(level);
            return deleted ? Results.NoContent() : Results.NotFound();
        }).RequireAuthorization();
    }
}

public record PositionLevelRequest(int Level, string? Position);
