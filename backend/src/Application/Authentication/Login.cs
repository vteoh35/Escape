namespace Application.Authentication;

public class Login
{
    private readonly IAuthenticationRepository _authenticationRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;

    public Login(
        IAuthenticationRepository authenticationRepository,
        IPasswordHasher passwordHasher,
        ITokenService tokenService)
    {
        _authenticationRepository = authenticationRepository;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
    }

    public string? Execute(string employeeId, string password)
    {
        var authentication = _authenticationRepository.GetByEmployeeId(employeeId);

        if (authentication == null || !_passwordHasher.Verify(password, authentication.PasswordHash))
        {
            return null;
        }

        return _tokenService.GenerateToken(employeeId);
    }
}
