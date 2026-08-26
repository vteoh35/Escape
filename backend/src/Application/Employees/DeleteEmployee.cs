namespace Application.Employees;

/// <summary>
/// Deletes an employee. Returns false if the id doesn't exist. Will fail at the database level if the employee still has dependent rows (e.g. an Authentication record).
/// </summary>
public class DeleteEmployee
{
    private readonly IEmployeeRepository _employeeRepository;

    public DeleteEmployee(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public bool Execute(string employeeId)
    {
        var employee = _employeeRepository.GetById(employeeId);

        if (employee == null)
        {
            return false;
        }

        _employeeRepository.Delete(employee);

        return true;
    }
}
