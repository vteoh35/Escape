# TODO: Automated tests

Read `docs/PROJECT_STATUS.md` first for overall context if you haven't.

## Current state

`src/tests/UnitTests/UnitTests.csproj` and `src/tests/ApiTests/ApiTests.csproj` exist as empty
project shells -- no test framework packages referenced, no test files. Zero automated test
coverage anywhere in the backend. Everything that's been verified so far (every repository, every
use case, the auth/authorization wiring, the exception middleware) was checked manually via
disposable scratch console projects run against the real Postgres database, then thrown away --
useful for one-time verification during development, but it leaves nothing behind to catch a
regression later.

## What's needed

**`UnitTests`** -- test the Application layer in isolation, with a fake/in-memory implementation of
each repository interface (don't hit real Postgres here). Good candidates to start with:
- Each `CreateX`/`UpdateX`/`DeleteX` use case: does it call the repository correctly, does
  `UpdateX`/`DeleteX` return null/false when the entity doesn't exist.
- `Infrastructure.PasswordHasher`: hash a password, verify it matches; verify a wrong password
  fails; verify two hashes of the same password differ (salted).
- `Application.Authorization.GetEmployeePermissions`: returns empty list for an employee with no
  role, returns the right permission names for one that has a role with permissions assigned.

**`ApiTests`** -- integration tests against a real (or `WebApplicationFactory`-hosted) instance of
the API, hitting real HTTP endpoints. This depends on the API endpoints actually being implemented
first (see the API TODO checklist in `program.cs`) -- there's not much to integration-test yet
beyond the Tasks endpoints, which are the one fully built example. Consider:
- Standard ASP.NET Core `WebApplicationFactory<Program>` pattern for spinning up the app in-process
  for tests, pointed at a test database (not the dev seed data database -- don't let tests mutate
  real seeded rows).
- Cover: the 401/403/200 authorization cases (this was manually verified once during development --
  see the "Wire up JWT authentication and permission-based authorization" commit -- turning that
  manual verification into a permanent automated test would directly prevent a regression there).
- Cover: the exception-middleware 500 behavior (e.g. POST a duplicate primary key, assert 500 +
  generic message, not a leaked stack trace).

## Packages needed

Neither test project has any packages yet. Typical choices for this kind of ASP.NET Core project:
`xunit` + `xunit.runner.visualstudio` (test framework), `Microsoft.NET.Test.Sdk`, `Moq` or
`NSubstitute` (mocking, for UnitTests), `Microsoft.AspNetCore.Mvc.Testing` (for
`WebApplicationFactory` in ApiTests).

## Verification

Run `dotnet test` from `backend/` once tests exist. CI (see `docs/TODO_deployment.md`) should run
this on every push/PR once it's set up -- don't wait for CI to exist before writing tests, but do
make sure `dotnet test` passes locally before considering this done.
