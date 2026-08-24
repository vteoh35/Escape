// TODO: implement Comments API endpoints.
// Follow the pattern in API/Tasks/TasksAPI.cs.
//
// Core CRUD (Application.Comments):
//   GET    /comments            -> GetComment.GetAll()
//   GET    /comments/{id}       -> GetComment.GetById(commentId)
//   POST   /comments            -> CreateComment.Execute(commentId, description, taskId, employeeId)
//   PUT    /comments/{id}       -> UpdateComment.Execute(commentId, description)
//   DELETE /comments/{id}       -> DeleteComment.Execute(commentId)
//
// Consider nesting under a task instead, e.g. GET /tasks/{taskId}/comments, if that fits the
// frontend's navigation better than a flat /comments collection.
//
// DI (program.cs): register ICommentRepository -> CommentRepository (AddScoped, needs AppDbContext),
// plus AddScoped for CreateComment/GetComment/UpdateComment/DeleteComment.
