# TODO: Frontend integration

Read `docs/PROJECT_STATUS.md` first for overall context if you haven't.

## Current state

Already in progress, on the frontend side. `frontend/` (Angular) has started building against
`mock-api/` (a separate in-memory ASP.NET Core project, not the real backend -- see
`mock-api/README.md`), via `frontend/src/environments/environment.ts`'s `apiUrl` and
`frontend/src/app/task.service.ts`. Only the Tasks feature is wired up on the frontend so far.

The real backend now has working endpoints for every feature (see `docs/PROJECT_STATUS.md` -> "API
surface"), not just Tasks -- the routes and response shapes were deliberately kept identical
between `mock-api/` and the real backend for exactly this reason. Switching a feature over is:

1. Change `environment.apiUrl` from `http://localhost:5100` (mock) to `http://localhost:5052` (real
   backend, or wherever it's actually hosted).
2. That's it for already-built features (Tasks) -- no other code should need to change.
3. For features the frontend hasn't built yet (everything except Tasks), build the Angular service
   the same way `task.service.ts` does, pointed at the routes listed in `docs/PROJECT_STATUS.md`.

## What's needed

1. Auth flow: call `POST /auth/login`, store the returned JWT, attach it as
   `Authorization: Bearer <token>` on subsequent requests. `POST /auth/register` exists too, for
   creating credentials for an existing employee.
2. Angular services for the remaining features (Projects, Employees, Comments, Attachments, Tags,
   Roles/Permissions, Priorities/Statuses/PositionLevels, ActivityLogs), following the
   `task.service.ts` pattern.
3. Eventually: point `environment.apiUrl` (and whatever the production equivalent is) at the real,
   deployed backend instead of the mock -- see `docs/TODO_deployment.md` for what "deployed" means
   for the backend right now (CI builds/tests it, but there's no hosting or CD yet).

## Not blocked anymore

Earlier versions of this doc said this was blocked on the real API endpoints existing. That's no
longer true -- every feature has real, verified endpoints now (see `docs/PROJECT_STATUS.md`). The
frontend can keep building against the mock for speed and swap individual features over to the real
backend whenever convenient, feature by feature.
