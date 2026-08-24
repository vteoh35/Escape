// TODO: implement ActivityLogs API endpoints.
// Follow the pattern in API/Tasks/TasksAPI.cs.
//
// Core CRUD (Application.ActivityLogs):
//   GET    /activity-logs            -> GetActivityLog.GetAll()
//   GET    /activity-logs/{id}       -> GetActivityLog.GetById(logId)
//   POST   /activity-logs            -> CreateActivityLog.Execute(logId, description, projectId, taskId, employeeId)
//                                        (LogTime is stamped automatically as DateTime.UtcNow -- don't take it as input)
//   PUT    /activity-logs/{id}       -> UpdateActivityLog.Execute(logId, description)
//   DELETE /activity-logs/{id}       -> DeleteActivityLog.Execute(logId)
//
// This is likely more useful called internally (e.g. from other endpoints, to record "task status
// changed" style audit events) than exposed as a public CRUD API for clients to write to directly --
// consider whether POST/PUT/DELETE should even be public routes, or just GET for viewing history.
//
// DI (program.cs): register IActivityLogRepository -> ActivityLogRepository (AddScoped, needs
// AppDbContext), plus AddScoped for CreateActivityLog/GetActivityLog/UpdateActivityLog/DeleteActivityLog.
