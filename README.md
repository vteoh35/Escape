# Escape

Task/project management app — ASP.NET Core (Minimal API) backend + Angular frontend, Postgres-backed.

## Structure

- `backend/` — ASP.NET Core REST API (Clean Architecture: Business Logic / Application / Infrastructure / API). See `backend/docs/PROJECT_STATUS.md` for what's built and what isn't.
- `frontend/` — Angular app.
- `mock-api/` — throwaway in-memory API for frontend development without a real database. See `mock-api/README.md`.

## First-time setup — backend

1. Install PostgreSQL locally, noting the password you set for the `postgres` user.
2. Create the database:
   ```powershell
   & "C:\Program Files\PostgreSQL\<version>\bin\psql.exe" -U postgres -h localhost -c "CREATE DATABASE escape_database;"
   ```
3. Configure secrets. **Never put these in an `appsettings.*.json` file** — they don't belong in git, even in a gitignored file (too easy to `git add -A` by accident). Use `dotnet user-secrets`, which stores them outside the repo entirely:
   ```powershell
   dotnet user-secrets init --project backend/src/API/Api.csproj
   dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=escape_database;Username=postgres;Password=<your-password>" --project backend/src/API/Api.csproj
   dotnet user-secrets set "Jwt:Key" "<any-random-string>" --project backend/src/API/Api.csproj
   ```
4. Install the EF Core CLI tool (one-time, machine-wide):
   ```powershell
   dotnet tool install --global dotnet-ef
   ```
5. Build the schema from the committed migrations:
   ```powershell
   dotnet ef database update --project backend/src/Infrastructure/Infrastructure.csproj --startup-project backend/src/API/Api.csproj
   ```
6. Optional — load sample data (safe to commit, all fake):
   ```powershell
   & "C:\Program Files\PostgreSQL\<version>\bin\psql.exe" -U postgres -h localhost -d escape_database -f backend/src/Infrastructure/Database/seed.sql
   ```
7. Run it:
   ```powershell
   $env:ASPNETCORE_ENVIRONMENT = "Development"
   dotnet run --project backend/src/API/Api.csproj --urls http://localhost:5052
   ```

## First-time setup — frontend

```powershell
cd frontend
npm install
npm start
```

Opens at `http://localhost:4200`. It talks to whichever API `frontend/src/environments/environment.ts` points at — defaults to the mock API. To run the mock API instead of the real backend:
```powershell
dotnet run --project mock-api --urls http://localhost:5100
```
Switch `apiUrl` in `environment.ts` to `http://localhost:5052` once your local backend (above) is running, or `http://localhost:5100` to go back to the mock.

## Workflow

- Don't push straight to `main` for anything beyond a trivial fix — branch, push, open a PR. Keeps two people from clobbering each other's work and gives CI a chance to catch problems before they land.
- CI/CD (`.github/workflows/ci.yml`) runs on every push/PR: builds and tests both the backend and the frontend. On a push to `main`, once both pass, it also triggers deploys to Render (backend) and Netlify (frontend) via their deploy hooks.
- Real secrets never go in git, gitignored file or not — `dotnet user-secrets` only (step 3 above).
