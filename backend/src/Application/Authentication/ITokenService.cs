namespace Application.Authentication;

/// <summary>
/// Issues a session token for an authenticated employee. Implemented in Infrastructure using JWT.
/// </summary>
public interface ITokenService
{
    string GenerateToken(string employeeId);
}
