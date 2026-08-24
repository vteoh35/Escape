namespace Application.Employees;

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
