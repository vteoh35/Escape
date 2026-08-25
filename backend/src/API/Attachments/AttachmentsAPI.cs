using Application.Attachments;

namespace API.Attachments;

public static class AttachmentsAPI
{
    public static void MapAttachmentEndpoints(this WebApplication app)
    {
        app.MapGet("/attachments", (GetAttachment getAttachment) => Results.Ok(getAttachment.GetAll()));

        app.MapGet("/attachments/{id}", (string id, GetAttachment getAttachment) =>
        {
            var attachment = getAttachment.GetById(id);
            return attachment == null ? Results.NotFound() : Results.Ok(attachment);
        });

        app.MapPost("/attachments", (CreateAttachmentRequest request, CreateAttachment createAttachment) =>
        {
            var attachment = createAttachment.Execute(request.AttachmentId, request.AttachmentLocation, request.ProjectId, request.TaskId);
            return Results.Created($"/attachments/{attachment.AttachmentId}", attachment);
        });

        app.MapPut("/attachments/{id}", (string id, UpdateAttachmentRequest request, UpdateAttachment updateAttachment) =>
        {
            var attachment = updateAttachment.Execute(id, request.AttachmentLocation);
            return attachment == null ? Results.NotFound() : Results.Ok(attachment);
        });

        app.MapDelete("/attachments/{id}", (string id, DeleteAttachment deleteAttachment) =>
        {
            var deleted = deleteAttachment.Execute(id);
            return deleted ? Results.NoContent() : Results.NotFound();
        });
    }
}

public record CreateAttachmentRequest(string AttachmentId, string AttachmentLocation, string? ProjectId, string? TaskId);
public record UpdateAttachmentRequest(string AttachmentLocation);
