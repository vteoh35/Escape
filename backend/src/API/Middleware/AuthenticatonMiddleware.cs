// TODO: implement JWT authentication.
//
// The signing infrastructure already exists in Infrastructure.Authentication.TokenService
// (Application.Authentication.ITokenService) -- it issues HMAC-SHA256 JWTs with a
// ClaimTypes.NameIdentifier claim holding the employee id. It takes the signing key as a plain
// constructor argument (not read from IConfiguration), so wiring it up means:
//
//   1. Add a signing key to appsettings.Development.json (gitignored) under e.g. "Jwt:Key",
//      and a real one via environment/secret store for other environments.
//   2. In program.cs, register the standard ASP.NET Core JWT bearer auth:
//        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
//            {
//                ValidateIssuerSigningKey = true,
//                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
//                ValidateIssuer = false,
//                ValidateAudience = false
//            });
//      and app.UseAuthentication() / app.UseAuthorization() before app.MapTaskEndpoints() etc.
//   3. Register ITokenService -> TokenService in DI, constructing it with the same key from config.
//   4. This file itself may not be needed once step 2 is in place (ASP.NET Core's built-in JWT
//      bearer middleware handles token validation) -- only build custom middleware here if you need
//      behavior beyond what AddJwtBearer gives you.
//
// The actual login endpoint (using Application.Authentication.Login /
// Application.Authentication.RegisterCredentials) isn't built yet either -- see
// API/Authentication/AuthenticationAPI.cs.
