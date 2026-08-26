using AuthenticationEntity = Business_Logic.Employees.Authentication;

namespace Application.Authentication;

/// <summary>
/// Data access contract for an employee's stored credentials, implemented in Infrastructure against Postgres.
/// </summary>
public interface IAuthenticationRepository
{
    AuthenticationEntity? GetByEmployeeId(string employeeId);
    void Add(AuthenticationEntity authentication);
    void Update(AuthenticationEntity authentication);
}
