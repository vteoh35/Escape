using Application.Comments;

namespace API.Comments;

/// <summary>
/// Comment endpoints: /comments.
/// </summary>
public static class CommentsAPI
{
    public static void MapCommentEndpoints(this WebApplication app)
    {
        app.MapGet("/comments", (GetComment getComment) => Results.Ok(getComment.GetAll()));

        app.MapGet("/comments/{id}", (string id, GetComment getComment) =>
        {
            var comment = getComment.GetById(id);
            return comment == null ? Results.NotFound() : Results.Ok(comment);
        });

        app.MapPost("/comments", (CreateCommentRequest request, CreateComment createComment) =>
        {
            var comment = createComment.Execute(request.CommentId, request.Description, request.TaskId, request.EmployeeId);
            return Results.Created($"/comments/{comment.CommentId}", comment);
        });

        app.MapPut("/comments/{id}", (string id, UpdateCommentRequest request, UpdateComment updateComment) =>
        {
            var comment = updateComment.Execute(id, request.Description);
            return comment == null ? Results.NotFound() : Results.Ok(comment);
        });

        app.MapDelete("/comments/{id}", (string id, DeleteComment deleteComment) =>
        {
            var deleted = deleteComment.Execute(id);
            return deleted ? Results.NoContent() : Results.NotFound();
        });
    }
}

public record CreateCommentRequest(string CommentId, string Description, string? TaskId, string? EmployeeId);
public record UpdateCommentRequest(string Description);
