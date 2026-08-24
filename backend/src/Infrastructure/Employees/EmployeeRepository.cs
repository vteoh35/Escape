using Application.Employees;
using Business_Logic.Employees;
using Infrastructure.Database;

namespace Infrastructure.Employees;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _context;

    public EmployeeRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<Employee> GetAll()
    {
        return _context.Employees.ToList();
    }

    public Employee? GetById(string employeeId)
    {
        return _context.Employees.FirstOrDefault(employee => employee.EmployeeId == employeeId);
    }

    public void Add(Employee employee)
    {
        _context.Employees.Add(employee);
        _context.SaveChanges();
    }

    public void Update(Employee employee)
    {
        _context.SaveChanges();
    }

    public void Delete(Employee employee)
    {
        _context.Employees.Remove(employee);
        _context.SaveChanges();
    }
}
