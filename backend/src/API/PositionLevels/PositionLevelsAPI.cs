// TODO: implement PositionLevels API endpoints.
// Follow the pattern in API/Tasks/TasksAPI.cs.
//
// Core CRUD (Application.PositionLevels):
//   GET    /position-levels            -> GetPositionLevel.GetAll()
//   GET    /position-levels/{level}    -> GetPositionLevel.GetByLevel(level)
//   POST   /position-levels            -> CreatePositionLevel.Execute(level, position)
//                                          (level is manually assigned, not DB-generated -- caller picks it;
//                                           position can be null)
//   PUT    /position-levels/{level}    -> UpdatePositionLevel.Execute(level, position)
//   DELETE /position-levels/{level}    -> DeletePositionLevel.Execute(level)
//
// This is small, mostly-static lookup data (3 seeded rows) referenced by Employee.EmployeeLevel --
// likely only needs admin-level CRUD, if any at all.
//
// DI (program.cs): register IPositionLevelRepository -> PositionLevelRepository (AddScoped, needs
// AppDbContext), plus AddScoped for CreatePositionLevel/GetPositionLevel/UpdatePositionLevel/DeletePositionLevel.
