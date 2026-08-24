using Business_Logic.Employees;

namespace Application.Employees;

public class CreateEmployee
{
    private readonly IEmployeeRepository _employeeRepository;

    public CreateEmployee(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public Employee Execute(string employeeId, string name, string email)
    {
        var employee = new Employee
        {
            EmployeeId = employeeId,
            Name = name,
            Email = email
        };

        _employeeRepository.Add(employee);

        return employee;
    }
}
