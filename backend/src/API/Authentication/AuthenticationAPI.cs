// TODO: implement Authentication API endpoints.
// Follow the pattern in API/Tasks/TasksAPI.cs.
//
// Application.Authentication:
//   POST /auth/register  -> RegisterCredentials.Execute(employeeId, password)
//                           (only meaningful for an employee row that already exists -- FK to employee)
//   POST /auth/login     -> Login.Execute(employeeId, password)
//                           returns a JWT string on success, null on bad credentials -> 401
//
// DI (program.cs): register
//   IAuthenticationRepository -> AuthenticationRepository
//   IPasswordHasher -> PasswordHasher
//   ITokenService -> TokenService  (constructor needs the signing key -- see
//     API/middleware/AuthenticatonMiddleware.cs for how that key should be sourced)
//   plus AddScoped for RegisterCredentials and Login.
//
// Once this exists, wire up API/middleware/AuthenticatonMiddleware.cs (JWT bearer validation) so
// other endpoints can require [Authorize] / require a valid token.
