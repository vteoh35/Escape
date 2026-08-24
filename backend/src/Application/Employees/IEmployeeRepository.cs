using Business_Logic.Employees;

namespace Application.Employees;

public interface IEmployeeRepository
{
    List<Employee> GetAll();
    Employee? GetById(string employeeId);
    void Add(Employee employee);
    void Update(Employee employee);
    void Delete(Employee employee);
}
