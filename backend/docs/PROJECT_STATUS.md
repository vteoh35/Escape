# Backend project status

Read this first if you're picking this project up cold (new session, new person). It tells you
what exists, what doesn't, and where to look next. The other files in this `docs/` folder are
detailed TODO briefs for specific remaining workstreams -- this file is the index.

## Architecture

Clean Architecture, four projects under `backend/src/`:

- **`Business Logic`** -- plain entity classes (e.g. `TaskItem`, `Employee`). No logic, no
  dependencies on anything else.
- **`Application`** -- use-case classes (`CreateX`/`GetX`/`UpdateX`/`DeleteX`) and repository
  interfaces (`IXRepository`). Depends only on Business Logic.
- **`Infrastructure`** -- EF Core (`AppDbContext`), repository implementations, auth/token/hashing
  services. Depends on Application (implements its interfaces) and Business Logic.
- **`API`** -- ASP.NET Core minimal API endpoints, middleware, `program.cs` (DI + pipeline wiring).
  Depends on Application and Infrastructure.

**Ownership split**: one person owns Business Logic + Application + Infrastructure, the other owns
API. This matters because the API layer is deliberately left with only TODO comments (no logic) in
files that aren't the API owner's -- don't fill those in unless you're told to; the point was to
hand off a clear spec, not to build it for them.

## Database

Real Postgres, already seeded with seed data before this work started (see `escape_database`,
connection string in `src/API/appsettings.Development.json`, gitignored -- ask for it if it's
missing). The schema was **hand-built with snake_case names before EF Core was introduced** --
`AppDbContext` maps onto that existing schema via Fluent API (table/column names, types, keys, FKs)
rather than EF generating its own schema. Entity class properties stay PascalCase C#; only the
Fluent API mapping in `AppDbContext.OnModelCreating` is snake_case-aware. Don't rename entity
properties to "match" the DB -- map them instead.

Migrations applied so far (`src/Infrastructure/Database/Migrations/`):
1. `InitialCreate` -- baseline, recorded as already-applied (tables pre-existed, migration wasn't
   run, just recorded in `__EFMigrationsHistory`)
2. `AddRolesAndPermissions` -- new `role`, `permission`, `role_permissions` tables + `employee.role_id`
3. `AddTags` -- new `tag`, `task_tags`, `project_tags` tables

To add more schema: edit `AppDbContext.OnModelCreating`, then
`dotnet ef migrations add <Name> --project src/Infrastructure/Infrastructure.csproj --startup-project src/API/Api.csproj --output-dir Database/Migrations`,
inspect the generated migration before applying, then
`dotnet ef database update --project src/Infrastructure/Infrastructure.csproj --startup-project src/API/Api.csproj`.
Always verify with `dotnet ef migrations has-pending-model-changes` (same args) after -- it should
say "No changes" once the model and migration agree.

## What's built (Business Logic + Application + Infrastructure)

Every entity in the schema has a full slice -- repository interface, EF-backed repository
implementation, and CRUD (or equivalent) use-case classes:

| Feature | Notes |
|---|---|
| Tasks | Converted from an in-memory placeholder to EF-backed |
| Projects | |
| Employees | |
| Comments | |
| Attachments | Just stores a location string -- no file upload/storage handling |
| ActivityLog | `LogTime` stamped automatically on create |
| Authentication | PBKDF2 password hashing (`PasswordHasher`), JWT issuance (`TokenService`) |
| Role / Permission (RBAC) | New schema this session -- `Employee.RoleId` -> `Role` -> `RolePermissions` -> `Permission` |
| Tags | Applies to both Tasks and Projects (`task_tags`, `project_tags`) |
| ProjectMember / TaskAssignee | "assign employee to X" join tables, with an optional free-text `Role` label (unrelated to the RBAC `Role` entity -- unfortunate naming collision, be aware) |
| Priority / Status / PositionLevel | Small static lookup tables; use manually-assigned ids (not DB-generated), unlike Role/Permission/Tag |
| **Authorization mechanism** | `Application.Authorization.GetEmployeePermissions` + `Infrastructure.Authorization.PermissionRequirement`/`PermissionAuthorizationHandler`, wired into ASP.NET Core's authorization framework via `program.cs`. Routes just call `.RequireAuthorization()` or pass a `PermissionRequirement` -- no further plumbing needed. |
| **Global exception handling** | `API/middleware/ExceptionMiddleware.cs` -- catches everything, returns a flat 500 with a safe message, logs the real exception server-side. Already wired first in the pipeline. |

Every one of the above was verified against **real Postgres data** during development (not mocked)
-- typically via a disposable scratch console project (`dotnet new console` referencing
`Infrastructure.csproj`, run against the real connection string, then deleted) since there was no
running API server to test through for most of this work. If you add something new, verify it the
same way before considering it done.

## What's NOT built yet

- **API endpoints** -- most `*API.cs` files under `src/API/` are TODO-only (see each file's
  comment for the exact routes/use-case mapping). `program.cs` has a checklist near the bottom of
  every `Map*Endpoints()` call and DI registration still needed. `TasksAPI.cs` is the one fully
  implemented example to copy the pattern from.
- **Input validation** -- see `docs/TODO_input_validation.md`
- **Automated tests** -- `src/tests/UnitTests` and `src/tests/ApiTests` exist as empty project
  shells with no test files. See `docs/TODO_testing.md`
- **Production configuration** -- only local dev config exists. See `docs/TODO_production_config.md`
- **Deployment** -- nothing (no Docker, no CI/CD, no hosting). See `docs/TODO_deployment.md`
- **Frontend integration** -- the Angular app in `frontend/` is untouched/unconnected to this
  backend. See `docs/TODO_frontend_integration.md`
- **Request logging middleware** -- `API/middleware/LoggingMiddleware.cs` is still TODO-only
  (unlike `ExceptionMiddleware`, which is done)
- **Tags entity** design note: only `Tag`/`TaskTag`/`ProjectTag` were built. `Role.cs` and
  `Permission.cs` under `Business_Logic/Employees/` are the RBAC entities (unrelated to Tags,
  despite Role/Permission sounding similar to Tag).

## Conventions to follow

- Repository interfaces live in `Application/<Feature>/I<Feature>Repository.cs`; implementations in
  `Infrastructure/<Feature>/<Feature>Repository.cs`, injected with `AppDbContext` and calling
  `SaveChanges()` after every mutation.
- Use-case classes are one class per action (`CreateX`, `GetX`, `UpdateX`, `DeleteX`), each taking
  the repository interface in its constructor and exposing an `Execute(...)` method (or
  `GetAll()`/`GetById(...)` for reads). No business logic lives in the repository -- it's pure
  data access.
- DI registration and endpoint mapping both happen in `program.cs`. Follow the Tasks block there as
  the template for wiring up a new feature.
- When an entity's key is DB-generated (identity), `Create` doesn't take the id as a parameter
  (Role, Permission, Tag). When it's manually assigned, it does (Task, Project, Employee, Priority,
  Status, PositionLevel).
