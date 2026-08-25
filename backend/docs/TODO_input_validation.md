# TODO: Input validation

Read `docs/PROJECT_STATUS.md` first for overall context if you haven't.

## Current state

Nothing validates request bodies before they reach the database. `TasksAPI.cs` (the one fully
implemented API file, in `src/API/Tasks/TasksAPI.cs`) will happily accept an empty-string `Name`,
an oversized `TaskId`, or a `Description` longer than the DB column's max length -- the failure
mode right now is either silent bad data getting saved, or (once truncation/constraint violations
happen) an unhandled `DbUpdateException` that `ExceptionMiddleware` turns into a generic 500. That's
better than a stack trace leaking, but a 500 for "you sent a blank name" is the wrong status code --
it should be a 400 with a message telling the caller what was wrong.

## What's needed

For each `POST`/`PUT` request record (e.g. `CreateTaskRequest`, `UpdateTaskRequest` in
`TasksAPI.cs`, and the equivalent records that'll exist once the other `*API.cs` files are
implemented), validate before calling into the Application-layer use case. Two reasonable
approaches, pick one and apply it consistently:

1. **Manual checks in the endpoint handler**, returning `Results.BadRequest(...)` early. Simplest,
   no new dependencies, but repetitive across every endpoint.
2. **Data annotations + a validation filter.** Add `[Required]`, `[MaxLength(N)]`, etc. to the
   request records, then either use a minimal API endpoint filter
   (`.AddEndpointFilter<ValidationFilter>()`) that runs `Validator.TryValidateObject` before the
   handler runs, or (if the ASP.NET Core version in use has it) the built-in
   `AddValidation()`/experimental minimal API validation support. Less repetitive, but adds
   structure that needs to be built once and then followed everywhere.

Whichever you pick, the max-length constraints should match what's actually in the DB schema (see
`AppDbContext.OnModelCreating` for `HasMaxLength(...)` calls per entity -- e.g. `TaskItem.Name` is
`HasMaxLength(150)`, `TaskItem.TaskId` is `HasMaxLength(10)`) so validation failures happen before
a DB round-trip, not instead of one.

## Where this intersects with ExceptionMiddleware

`ExceptionMiddleware` (`src/API/middleware/ExceptionMiddleware.cs`) already returns a flat 500 for
anything unhandled. Once validation exists, most "bad input" cases should be caught by validation
(400) before they ever reach the DB layer and become an unhandled exception. Don't try to make
`ExceptionMiddleware` smarter about mapping DB constraint violations to 400s as a substitute for
real input validation -- that's treating a symptom, not the cause, and DB error messages aren't
reliable/stable enough to parse for that purpose.

## Verification

Once built, test against the real API (not mocks) the same way earlier work in this project was
verified: run `dotnet run --project src/API`, `curl` a deliberately bad request (empty name,
oversized id, etc.), confirm you get a 400 with a useful message instead of a 500 or a silently
saved bad row. Check the DB directly (`psql`) to confirm nothing bad landed.
