# Escape

[![CI/CD](https://github.com/vteoh35/Escape/actions/workflows/cicd.yml/badge.svg)](https://github.com/vteoh35/Escape/actions/workflows/cicd.yml)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

A project and task management app for teams — organize work into projects, break projects into
tasks (with sub-tasks), assign teammates, track status/priority, and collaborate through comments,
attachments, and tags. Access is controlled by an admin-configurable role/permission system, and
every notable action is recorded in an activity log.

**Live**: backend API at [escape-43af.onrender.com](https://escape-43af.onrender.com) (Render) ·
frontend deployed via Netlify.

## Features

- **Projects & Tasks** — full CRUD, with tasks nestable under a parent task (sub-tasks) and scoped
  to a project
- **Team assignment** — assign employees to projects and tasks, each with an optional role label
  (e.g. "Project Manager")
- **Comments** — threaded discussion on tasks (replies to replies)
- **Attachments** — file/link references on projects and tasks
- **Tags** — apply labels to both projects and tasks
- **Activity log** — an audit trail of what happened, when, on which project/task/employee
- **Authentication** — JWT-based login, PBKDF2 password hashing
- **Role-based access control (RBAC)** — admin-configurable roles and permissions, not a fixed
  hardcoded set; permission checks are enforced via ASP.NET Core's authorization framework
- **Lookup data** — configurable Priority, Status, and Position Level lists

## Tech stack

| Layer | Tech |
|---|---|
| Backend | C# / .NET 10, ASP.NET Core (Minimal APIs), Entity Framework Core, Npgsql |
| Database | PostgreSQL |
| Auth | JWT (`Microsoft.AspNetCore.Authentication.JwtBearer`), PBKDF2 password hashing |
| Frontend | Angular 21, TypeScript, Signals, RxJS |
| Testing | Vitest (frontend) |
| CI/CD | GitHub Actions — build + test on every push/PR, auto-deploy on push to `main` |
| Hosting | Render (backend, via Docker), Netlify (frontend) |

## Architecture

The backend follows Clean Architecture, split into four projects under `backend/src/`:

```
Business Logic  →  Application  →  Infrastructure  →  API
   (entities)      (use cases,      (EF Core,          (HTTP endpoints,
                  repository        repositories,        middleware,
                  interfaces)       auth services)        DI wiring)
```

Each layer only depends on the ones to its left. See
[`backend/docs/PROJECT_STATUS.md`](backend/docs/PROJECT_STATUS.md) for the full breakdown of what's
built, the complete API route table, and conventions to follow when adding to it.

## Project structure

```
backend/     ASP.NET Core REST API (see backend/docs/PROJECT_STATUS.md)
frontend/    Angular app
mock-api/    Throwaway in-memory API for frontend dev without a real database (see mock-api/README.md)
Dockerfile   Multi-stage build for the backend (used by Render)
netlify.toml Frontend deploy config (used by Netlify)
```

## Getting started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/download/)
- [Node.js](https://nodejs.org/) + npm

### Backend

1. Create the database:
   ```powershell
   & "C:\Program Files\PostgreSQL\<version>\bin\psql.exe" -U postgres -h localhost -c "CREATE DATABASE escape_database;"
   ```
2. Configure secrets with `dotnet user-secrets` — **never** in an `appsettings.*.json` file, gitignored
   or not (too easy to `git add -A` by accident):
   ```powershell
   dotnet user-secrets init --project backend/src/API/Api.csproj
   dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=escape_database;Username=postgres;Password=<your-password>" --project backend/src/API/Api.csproj
   dotnet user-secrets set "Jwt:Key" "<any-random-string>" --project backend/src/API/Api.csproj
   ```
3. Install the EF Core CLI tool (one-time, machine-wide):
   ```powershell
   dotnet tool install --global dotnet-ef
   ```
4. Build the schema from the committed migrations:
   ```powershell
   dotnet ef database update --project backend/src/Infrastructure/Infrastructure.csproj --startup-project backend/src/API/Api.csproj
   ```
5. Optional — load sample data (safe to commit, all fake):
   ```powershell
   & "C:\Program Files\PostgreSQL\<version>\bin\psql.exe" -U postgres -h localhost -d escape_database -f backend/src/Infrastructure/Database/seed.sql
   ```
6. Run it:
   ```powershell
   $env:ASPNETCORE_ENVIRONMENT = "Development"
   dotnet run --project backend/src/API/Api.csproj --urls http://localhost:5052
   ```

### Frontend

```powershell
cd frontend
npm install
npm start
```

Opens at `http://localhost:4200`. It talks to whichever API `frontend/src/environments/environment.ts`
points at — defaults to the mock API. To run the mock API instead of the real backend:
```powershell
dotnet run --project mock-api --urls http://localhost:5100
```
Switch `apiUrl` in `environment.ts` to `http://localhost:5052` once your local backend is running,
or back to `http://localhost:5100` for the mock.

## API

The full route table (every resource, every endpoint) lives in
[`backend/docs/PROJECT_STATUS.md`](backend/docs/PROJECT_STATUS.md#api-surface). Quick example:

```
GET    /tasks              List tasks
POST   /tasks               Create a task
GET    /tasks/{id}          Get one task
PUT    /tasks/{id}          Update a task
DELETE /tasks/{id}          Delete a task
POST   /auth/login          { employeeId, password } -> { token }
```

## Testing

CI runs `dotnet test` (backend) and `npm test` (frontend) on every push/PR. Backend test projects
exist but don't have real tests yet — see
[`backend/docs/TODO_testing.md`](backend/docs/TODO_testing.md).

## Deployment

`.github/workflows/cicd.yml` builds and tests both apps on every push/PR. On a push to `main`, once
both pass, it triggers deploy hooks for Render (backend, via the root `Dockerfile`) and Netlify
(frontend, via `netlify.toml`). See
[`backend/docs/TODO_deployment.md`](backend/docs/TODO_deployment.md) for what's still open (e.g.
running DB migrations automatically as part of a deploy, a health check endpoint).

## Documentation

- [`backend/docs/PROJECT_STATUS.md`](backend/docs/PROJECT_STATUS.md) — architecture, what's built,
  full API route table, conventions
- [`backend/docs/`](backend/docs/) — topic-specific TODOs (input validation, testing, production
  config, deployment, frontend integration)
- [`mock-api/README.md`](mock-api/README.md) — the mock API used for frontend development

## Workflow

- Don't push straight to `main` for anything beyond a trivial fix — branch, push, open a PR. Keeps
  two people from clobbering each other's work and gives CI a chance to catch problems first.
- Real secrets never go in git, gitignored file or not — `dotnet user-secrets` only.

## License

[MIT](LICENSE)
