using Application.Authentication;

namespace API.Authentication;

/// <summary>
/// Login/registration endpoints: /auth/register, /auth/login.
/// </summary>
public static class AuthenticationAPI
{
    public static void MapAuthenticationEndpoints(this WebApplication app)
    {
        app.MapPost("/auth/register", (RegisterRequest request, RegisterCredentials registerCredentials) =>
        {
            var authentication = registerCredentials.Execute(request.EmployeeId, request.Password);
            return Results.Created($"/employees/{authentication.EmployeeId}", new { authentication.EmployeeId });
        });

        app.MapPost("/auth/login", (LoginRequest request, Login login) =>
        {
            var token = login.Execute(request.EmployeeId, request.Password);
            return token == null ? Results.Unauthorized() : Results.Ok(new { token });
        });
    }
}

public record RegisterRequest(string EmployeeId, string Password);
public record LoginRequest(string EmployeeId, string Password);
