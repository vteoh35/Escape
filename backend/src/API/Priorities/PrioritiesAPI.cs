// TODO: implement Priorities API endpoints.
// Follow the pattern in API/Tasks/TasksAPI.cs.
//
// Core CRUD (Application.Priorities):
//   GET    /priorities            -> GetPriority.GetAll()
//   GET    /priorities/{id}       -> GetPriority.GetById(priorityId)
//   POST   /priorities            -> CreatePriority.Execute(priorityId, priorityName)
//                                     (priorityId is manually assigned, not DB-generated -- caller picks it)
//   PUT    /priorities/{id}       -> UpdatePriority.Execute(priorityId, priorityName)
//   DELETE /priorities/{id}       -> DeletePriority.Execute(priorityId)
//
// This is small, mostly-static lookup data (3 seeded rows: Low/Medium/High) referenced by
// Task.PriorityId and Project.PriorityId -- likely only needs admin-level CRUD, if any at all.
//
// DI (program.cs): register IPriorityRepository -> PriorityRepository (AddScoped, needs
// AppDbContext), plus AddScoped for CreatePriority/GetPriority/UpdatePriority/DeletePriority.
