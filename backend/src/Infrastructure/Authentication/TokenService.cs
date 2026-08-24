using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Authentication;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Authentication;

public class TokenService : ITokenService
{
    private readonly string _signingKey;
    private readonly TimeSpan _tokenLifetime;

    public TokenService(string signingKey, TimeSpan? tokenLifetime = null)
    {
        _signingKey = signingKey;
        _tokenLifetime = tokenLifetime ?? TimeSpan.FromHours(8);
    }

    public string GenerateToken(string employeeId)
    {
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_signingKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: new[] { new Claim(ClaimTypes.NameIdentifier, employeeId) },
            expires: DateTime.UtcNow.Add(_tokenLifetime),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
