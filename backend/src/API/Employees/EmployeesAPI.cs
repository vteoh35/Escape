// TODO: implement Employees API endpoints.
// Follow the pattern in API/Tasks/TasksAPI.cs.
//
// Core CRUD (Application.Employees):
//   GET    /employees            -> GetEmployee.GetAll()
//   GET    /employees/{id}       -> GetEmployee.GetById(employeeId)
//   POST   /employees            -> CreateEmployee.Execute(employeeId, name, email)
//   PUT    /employees/{id}       -> UpdateEmployee.Execute(employeeId, name, email)
//   DELETE /employees/{id}       -> DeleteEmployee.Execute(employeeId)
//
// Consider also exposing (optional, not built yet as endpoints):
//   PUT /employees/{id}/role -> set Employee.RoleId directly via AppDbContext, or add a small
//   Application.Employees.AssignRole use case if you want it going through a use case class.
//
// DI (program.cs): register IEmployeeRepository -> EmployeeRepository (AddScoped, needs AppDbContext),
// plus AddScoped for CreateEmployee/GetEmployee/UpdateEmployee/DeleteEmployee.
