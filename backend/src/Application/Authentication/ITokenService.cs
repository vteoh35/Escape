namespace Application.Authentication;

public interface ITokenService
{
    string GenerateToken(string employeeId);
}
