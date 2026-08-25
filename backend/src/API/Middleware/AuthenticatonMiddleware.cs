// DONE (in program.cs, not this file): JWT bearer authentication is wired up via
// AddAuthentication().AddJwtBearer(...) + app.UseAuthentication()/app.UseAuthorization().
// Permission-based authorization is also wired up (Infrastructure.Authorization.
// PermissionRequirement + PermissionAuthorizationHandler, backed by
// Application.Authorization.GetEmployeePermissions).
//
// This file isn't needed as custom middleware -- ASP.NET Core's built-in JWT bearer handler
// covers token validation. See program.cs for the wiring, and its TODO comment near the bottom
// for exact syntax to require auth / a specific permission on a route.
//
// Still TODO: the actual /auth/login and /auth/register endpoints (Application.Authentication.Login
// and Application.Authentication.RegisterCredentials exist, but nothing calls them yet) --
// see API/Authentication/AuthenticationAPI.cs.
