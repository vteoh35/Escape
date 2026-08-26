using Application.Authentication;
using Infrastructure.Database;
using AuthenticationEntity = Business_Logic.Employees.Authentication;

namespace Infrastructure.Authentication;

/// <summary>
/// EF Core-backed implementation of IAuthenticationRepository.
/// </summary>
public class AuthenticationRepository : IAuthenticationRepository
{
    private readonly AppDbContext _context;

    public AuthenticationRepository(AppDbContext context)
    {
        _context = context;
    }

    public AuthenticationEntity? GetByEmployeeId(string employeeId)
    {
        return _context.Authentications.FirstOrDefault(authentication => authentication.EmployeeId == employeeId);
    }

    public void Add(AuthenticationEntity authentication)
    {
        _context.Authentications.Add(authentication);
        _context.SaveChanges();
    }

    public void Update(AuthenticationEntity authentication)
    {
        _context.SaveChanges();
    }
}
