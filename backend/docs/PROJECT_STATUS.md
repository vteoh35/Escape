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

**Ownership split**: originally one person owned Business Logic + Application + Infrastructure, the
other owned API, with the API layer left as TODO comments as a handoff spec. To get to a deployable
backend quickly, the API layer was then fully implemented by the same person who did the other three
layers -- see "What's built" below, all of it is real now, not a spec.

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
| **Global exception handling** | `API/middleware/ExceptionMiddleware.cs` -- catches everything, returns a flat 500 with a safe message, logs the real exception server-side. Wired first in the pipeline. |
| **Request logging** | `API/middleware/LoggingMiddleware.cs` -- logs method/path/status/duration for every request. |
| **API layer** | Every feature above has real, working HTTP endpoints in `src/API/<Feature>/<Feature>API.cs` (not TODO comments) -- see "API surface" below. |

Every one of the above was verified against **real Postgres data** (not mocked) -- either via a
disposable scratch console project during earlier development, or (for the full API layer) by
actually running `dotnet run --project src/API` and hitting real routes with `curl`, including a
full register -> wrong-password-401 -> correct-password-200-with-JWT flow, nested routes
(`/projects/{id}/members`, `/tasks/{id}/assignees`, etc.), and confirming `ExceptionMiddleware`
turns a real FK-violation `DbUpdateException` into a clean 500 instead of a leaked stack trace. If
you add something new, verify it the same way before considering it done.

## API surface

Base URL when running locally: `http://localhost:5052` (or whatever `--urls` you pass). All routes
are currently unauthenticated by default (no `.RequireAuthorization()` calls yet) -- see the note
at the bottom of `program.cs` for how to add auth/permission requirements to a route once that's a
product decision someone wants to make.

| Resource | Routes |
|---|---|
| Tasks | `GET/POST /tasks`, `GET/PUT/DELETE /tasks/{id}`, `GET/POST /tasks/{id}/assignees`, `PUT/DELETE /tasks/{id}/assignees/{employeeId}`, `GET /tasks/{id}/tags`, `POST/DELETE /tasks/{id}/tags/{tagId}` |
| Projects | `GET/POST /projects`, `GET/PUT/DELETE /projects/{id}`, `GET/POST /projects/{id}/members`, `PUT/DELETE /projects/{id}/members/{employeeId}`, `GET /projects/{id}/tags`, `POST/DELETE /projects/{id}/tags/{tagId}` |
| Employees | `GET/POST /employees`, `GET/PUT/DELETE /employees/{id}` |
| Comments | `GET/POST /comments`, `GET/PUT/DELETE /comments/{id}` |
| Attachments | `GET/POST /attachments`, `GET/PUT/DELETE /attachments/{id}` |
| Tags | `GET/POST /tags`, `GET/PUT/DELETE /tags/{id}` |
| Authentication | `POST /auth/register`, `POST /auth/login` (returns `{ token }` or 401) |
| Activity logs | `GET/POST /activity-logs`, `GET/PUT/DELETE /activity-logs/{id}` |
| Roles | `GET/POST /roles`, `GET/PUT/DELETE /roles/{id}`, `GET /roles/{id}/permissions`, `POST/DELETE /roles/{id}/permissions/{permissionId}` |
| Permissions | `GET/POST /permissions`, `GET/PUT/DELETE /permissions/{id}` |
| Priorities | `GET/POST /priorities`, `GET/PUT/DELETE /priorities/{id}` |
| Statuses | `GET/POST /statuses`, `GET/PUT/DELETE /statuses/{id}` |
| Position levels | `GET/POST /position-levels`, `GET/PUT/DELETE /position-levels/{level}` |

Identity-generated ids (Role, Permission, Tag) don't take an id in the `POST` body -- the DB
assigns it. Everything else (Task, Project, Employee, Comment, Attachment, ActivityLog, Priority,
Status, PositionLevel) uses a manually-assigned id, so the client provides it.

## What's NOT built yet

- **Input validation** -- see `docs/TODO_input_validation.md`. Bad input mostly surfaces as a
  generic 500 via `ExceptionMiddleware` right now (e.g. a duplicate id is a real DB constraint
  violation, correctly caught, but a 400 with a clear message would be better than a 500).
- **Automated tests** -- `src/tests/UnitTests` and `src/tests/ApiTests` exist as empty (literally
  0-byte) project files, not even valid `.csproj`s -- deliberately left out of `escape.sln` for now
  since including them breaks the build. See `docs/TODO_testing.md`.
- **Production configuration** -- only local dev config exists (`appsettings.Development.json`,
  gitignored). See `docs/TODO_production_config.md`.
- **Deployment** -- CI (`.github/workflows/ci.yml`) builds and tests `escape.sln` on push/PR, but
  there's no Dockerfile, no CD, no hosting decided. See `docs/TODO_deployment.md`.
- **Frontend integration** -- in progress on the frontend side against `mock-api/` (see
  `frontend/src/environments/environment.ts`); the real backend endpoints above now exist for every
  feature, not just Tasks, so the frontend can be pointed at the real backend (`http://localhost:5052`)
  feature-by-feature as it's ready. See `docs/TODO_frontend_integration.md`.
- **Route-level permission enforcement** -- the RBAC mechanism works (verified), but no route
  currently calls `.RequireAuthorization(...)`. Deciding which endpoints need which permission is a
  product decision, intentionally left open.
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
