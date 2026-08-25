# TODO: Frontend integration

Read `docs/PROJECT_STATUS.md` first for overall context if you haven't.

## Current state

`frontend/` is a separate Angular application, untouched by and unconnected to any of the backend
work described in this `docs/` folder. `program.cs` already has a CORS policy named `"Frontend"`
allowing `http://localhost:4200` (the Angular dev server default), so the plumbing for local dev is
in place, but nothing in the frontend actually calls this API yet, as far as the backend work here
knows.

## What's needed

This is frontend work, not backend work -- listed here mainly so a fresh session doesn't assume
it's out of scope entirely or forget it needs to happen eventually. Roughly:

1. An HTTP client service in the Angular app pointed at this API's base URL (configurable per
   environment -- local dev vs. wherever this ends up deployed, see `docs/TODO_deployment.md`).
2. Whatever auth flow the frontend needs to call `/auth/login` (once built -- see
   `API/Authentication/AuthenticationAPI.cs`'s TODO) and attach the returned JWT as a
   `Authorization: Bearer <token>` header on subsequent requests.
3. Once real API endpoints exist beyond Tasks (see the checklist in `program.cs`), wire up whatever
   frontend views/components need them.

## Blockers

This is blocked on the API endpoints actually being built (currently only `TasksAPI.cs` is
implemented -- see `program.cs`'s TODO checklist and `docs/PROJECT_STATUS.md`) and on the
login/register endpoints existing (`docs/PROJECT_STATUS.md` -> Authentication row notes what's
built vs. missing there). Don't start frontend integration work assuming endpoints exist that are
still just TODO comments -- check the actual file content, not just the file's existence.
