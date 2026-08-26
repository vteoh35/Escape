using AuthenticationEntity = Business_Logic.Employees.Authentication;

namespace Application.Authentication;

/// <summary>
/// Sets a password (hashed) for an existing employee, creating their Authentication record.
/// </summary>
public class RegisterCredentials
{
    private readonly IAuthenticationRepository _authenticationRepository;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterCredentials(IAuthenticationRepository authenticationRepository, IPasswordHasher passwordHasher)
    {
        _authenticationRepository = authenticationRepository;
        _passwordHasher = passwordHasher;
    }

    public AuthenticationEntity Execute(string employeeId, string password)
    {
        var authentication = new AuthenticationEntity
        {
            EmployeeId = employeeId,
            PasswordHash = _passwordHasher.Hash(password)
        };

        _authenticationRepository.Add(authentication);

        return authentication;
    }
}
