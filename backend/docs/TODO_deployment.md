# TODO: Deployment

Read `docs/PROJECT_STATUS.md` first for overall context if you haven't.

## Current state

`.github/workflows/ci.yml` builds and tests `escape.sln` (backend) and `frontend/` on every
push/PR to main. `escape.sln` now actually references the real backend projects (Business Logic,
Application, Infrastructure, API) -- it was empty before, which meant CI was passing without
building anything; that's fixed. `UnitTests`/`ApiTests` are still empty 0-byte files (not valid
projects), so they're deliberately left out of the solution -- adding them back in once they have
real content is part of `docs/TODO_testing.md`, not this doc.

Still nothing beyond that: no `Dockerfile`, no CD, no hosting target decided or configured. The app
has only ever been run with `dotnet run --project src/API` against a local Postgres instance.

## What's needed (roughly in order)

1. **Decide where this actually runs.** This doc deliberately doesn't prescribe a host -- that's a
   product/infra decision (Azure, AWS, a plain VM, Railway/Render/Fly.io, etc.), not something to
   guess at from the code. Whoever picks this up should confirm the target with the team before
   building deployment config around a guess.
2. **Containerize**, if the chosen host wants a container (most do). A `Dockerfile` for
   `src/API/Api.csproj` -- standard multi-stage .NET build (`dotnet publish` in a build stage, copy
   output into a slim runtime image). Watch for: the project structure has spaces in a folder name
   (`Business Logic`) -- make sure Docker COPY/build steps handle that correctly (quote paths).
3. **Database migrations in the deploy pipeline.** Right now migrations are applied manually via
   `dotnet ef database update`. Decide how that happens for a real deployment -- e.g. run migrations
   as a startup step, or a separate pipeline step before the app starts, so schema changes ship
   safely instead of relying on someone remembering to run the command by hand.
4. **CD**: deploy on merge to main (or a release process, if a more controlled rollout is wanted).
   Depends entirely on the hosting decision from step 1. CI (build + test) already exists; this is
   the missing "and then actually deploy it" half.
5. Health check endpoint for whatever platform needs one (most container hosts/load balancers want
   a `/health`-style route to confirm the app is up) -- trivial to add once needed, not worth
   building speculatively before the host is chosen.

## Dependencies on other TODOs

This can't be finished without `docs/TODO_production_config.md` (secrets have to come from
somewhere in the deployed environment) being addressed alongside it -- they're two halves of the
same problem. Tests from `docs/TODO_testing.md` aren't a hard blocker for a first deployment, but
CI without any tests to run is much less valuable.
