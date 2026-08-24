using AuthenticationEntity = Business_Logic.Employees.Authentication;

namespace Application.Authentication;

public interface IAuthenticationRepository
{
    AuthenticationEntity? GetByEmployeeId(string employeeId);
    void Add(AuthenticationEntity authentication);
    void Update(AuthenticationEntity authentication);
}
