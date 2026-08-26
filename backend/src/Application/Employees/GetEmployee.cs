using Business_Logic.Employees;

namespace Application.Employees;

/// <summary>
/// Reads employees, either all of them or by id.
/// </summary>
public class GetEmployee
{
    private readonly IEmployeeRepository _employeeRepository;

    public GetEmployee(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public List<Employee> GetAll()
    {
        return _employeeRepository.GetAll();
    }

    public Employee? GetById(string employeeId)
    {
        return _employeeRepository.GetById(employeeId);
    }
}
