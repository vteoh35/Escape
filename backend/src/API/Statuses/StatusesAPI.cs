// TODO: implement Statuses API endpoints.
// Follow the pattern in API/Tasks/TasksAPI.cs.
//
// Core CRUD (Application.Statuses):
//   GET    /statuses            -> GetStatus.GetAll()
//   GET    /statuses/{id}       -> GetStatus.GetById(statusId)
//   POST   /statuses            -> CreateStatus.Execute(statusId, statusName)
//                                   (statusId is manually assigned, not DB-generated -- caller picks it)
//   PUT    /statuses/{id}       -> UpdateStatus.Execute(statusId, statusName)
//   DELETE /statuses/{id}       -> DeleteStatus.Execute(statusId)
//
// This is small, mostly-static lookup data (3 seeded rows: To Do/In Progress/Completed) referenced
// by Task.StatusId and Project.StatusId -- likely only needs admin-level CRUD, if any at all.
//
// DI (program.cs): register IStatusRepository -> StatusRepository (AddScoped, needs AppDbContext),
// plus AddScoped for CreateStatus/GetStatus/UpdateStatus/DeleteStatus.
