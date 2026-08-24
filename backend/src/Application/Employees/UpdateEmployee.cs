using Business_Logic.Employees;

namespace Application.Employees;

public class UpdateEmployee
{
    private readonly IEmployeeRepository _employeeRepository;

    public UpdateEmployee(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public Employee? Execute(string employeeId, string name, string email)
    {
        var employee = _employeeRepository.GetById(employeeId);

        if (employee == null)
        {
            return null;
        }

        employee.Name = name;
        employee.Email = email;

        _employeeRepository.Update(employee);

        return employee;
    }
}
