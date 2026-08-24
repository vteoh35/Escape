// TODO: implement Attachments API endpoints.
// Follow the pattern in API/Tasks/TasksAPI.cs.
//
// Core CRUD (Application.Attachments):
//   GET    /attachments            -> GetAttachment.GetAll()
//   GET    /attachments/{id}       -> GetAttachment.GetById(attachmentId)
//   POST   /attachments            -> CreateAttachment.Execute(attachmentId, attachmentLocation, projectId, taskId)
//   PUT    /attachments/{id}       -> UpdateAttachment.Execute(attachmentId, attachmentLocation)
//   DELETE /attachments/{id}       -> DeleteAttachment.Execute(attachmentId)
//
// Note: AttachmentLocation is just a string (e.g. a file path/URL) in the current model -- there's
// no file upload/storage handling built yet. If real file uploads are needed, that's new scope
// (e.g. saving to disk/blob storage and storing the resulting location here).
//
// DI (program.cs): register IAttachmentRepository -> AttachmentsRepository (AddScoped, needs AppDbContext),
// plus AddScoped for CreateAttachment/GetAttachment/UpdateAttachment/DeleteAttachment.
