using Application.Tags;

namespace API.Tags;

public static class TagsAPI
{
    public static void MapTagEndpoints(this WebApplication app)
    {
        app.MapGet("/tags", (GetTag getTag) => Results.Ok(getTag.GetAll()));

        app.MapGet("/tags/{id}", (int id, GetTag getTag) =>
        {
            var tag = getTag.GetById(id);
            return tag == null ? Results.NotFound() : Results.Ok(tag);
        });

        app.MapPost("/tags", (CreateTagRequest request, CreateTag createTag) =>
        {
            var tag = createTag.Execute(request.TagName);
            return Results.Created($"/tags/{tag.TagId}", tag);
        });

        app.MapPut("/tags/{id}", (int id, CreateTagRequest request, UpdateTag updateTag) =>
        {
            var tag = updateTag.Execute(id, request.TagName);
            return tag == null ? Results.NotFound() : Results.Ok(tag);
        });

        app.MapDelete("/tags/{id}", (int id, DeleteTag deleteTag) =>
        {
            var deleted = deleteTag.Execute(id);
            return deleted ? Results.NoContent() : Results.NotFound();
        });

        app.MapGet("/tasks/{id}/tags", (string id, GetTaskTags getTaskTags) =>
            Results.Ok(getTaskTags.Execute(id)));

        app.MapPost("/tasks/{id}/tags/{tagId}", (string id, int tagId, TagTask tagTask) =>
        {
            var taskTag = tagTask.Execute(id, tagId);
            return Results.Created($"/tasks/{id}/tags/{tagId}", taskTag);
        });

        app.MapDelete("/tasks/{id}/tags/{tagId}", (string id, int tagId, UntagTask untagTask) =>
        {
            var removed = untagTask.Execute(id, tagId);
            return removed ? Results.NoContent() : Results.NotFound();
        });
    }
}

public record CreateTagRequest(string TagName);
