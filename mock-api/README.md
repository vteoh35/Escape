# Mock API

A throwaway ASP.NET Core server for frontend development -- lets the Angular app build out
layout, components, and user flows without waiting on the real backend endpoints (most of which
are still just TODO comments -- see `backend/docs/PROJECT_STATUS.md`).

Not part of the real backend. Not connected to Postgres. All data is in-memory and resets every
time you restart the process.

## Run it

```bash
dotnet run --project mock-api --urls http://localhost:5100
```

Then point the Angular app's API base URL at `http://localhost:5100`. CORS is already configured
to allow `http://localhost:4200` (the Angular dev server default).

## What it covers

Every route documented in the real backend's `backend/src/API/*/**.cs` TODO comments and
`backend/src/API/program.cs` checklist, with realistic fake data and full CRUD (GET/POST/PUT/DELETE)
backed by in-memory lists: tasks, projects (+ members, + tags), employees, comments, attachments,
tags, activity logs, roles (+ permissions), permissions, priorities, statuses, position levels, and
a mock `/auth/login` / `/auth/register` (accepts anything, always "succeeds" -- it's not real
auth, just enough to build a login screen against).

Response shapes match the real backend's `Business_Logic` entities exactly (same field names,
same JSON casing) -- see `backend/docs/PROJECT_STATUS.md` for what those entities are.

## Switching to the real backend later

Change the frontend's API base URL to point at the real backend instead of this project. No other
frontend code should need to change, since routes and shapes match. (The real backend's endpoints
have to actually be implemented first -- see `backend/src/API/program.cs`'s TODO checklist for
what's still missing there.)

## Don't

- Don't add real authentication/authorization here -- that defeats the point of a mock, and the
  real mechanism already exists in the real backend (see `backend/docs/PROJECT_STATUS.md`).
- Don't wire this up to Postgres or any real database.
- Don't treat this as a source of truth for what the real API will do -- it mirrors the *documented
  plan* in the real backend's TODO comments, which could change before those endpoints are
  actually built.
