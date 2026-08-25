using Application.Employees;

namespace API.Employees;

public static class EmployeesAPI
{
    public static void MapEmployeeEndpoints(this WebApplication app)
    {
        app.MapGet("/employees", (GetEmployee getEmployee) => Results.Ok(getEmployee.GetAll()));

        app.MapGet("/employees/{id}", (string id, GetEmployee getEmployee) =>
        {
            var employee = getEmployee.GetById(id);
            return employee == null ? Results.NotFound() : Results.Ok(employee);
        });

        app.MapPost("/employees", (CreateEmployeeRequest request, CreateEmployee createEmployee) =>
        {
            var employee = createEmployee.Execute(request.EmployeeId, request.Name, request.Email);
            return Results.Created($"/employees/{employee.EmployeeId}", employee);
        }).RequireAuthorization();

        app.MapPut("/employees/{id}", (string id, UpdateEmployeeRequest request, UpdateEmployee updateEmployee) =>
        {
            var employee = updateEmployee.Execute(id, request.Name, request.Email);
            return employee == null ? Results.NotFound() : Results.Ok(employee);
        }).RequireAuthorization();

        app.MapDelete("/employees/{id}", (string id, DeleteEmployee deleteEmployee) =>
        {
            var deleted = deleteEmployee.Execute(id);
            return deleted ? Results.NoContent() : Results.NotFound();
        }).RequireAuthorization();
    }
}

public record CreateEmployeeRequest(string EmployeeId, string Name, string Email);
public record UpdateEmployeeRequest(string Name, string Email);
