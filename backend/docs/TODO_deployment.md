# TODO: Deployment

Read `docs/PROJECT_STATUS.md` first for overall context if you haven't.

## Current state

`.github/workflows/cicd.yml` builds and tests `escape.sln` (backend) and `frontend/` on every
push/PR to main. `escape.sln` now actually references the real backend projects (Business Logic,
Application, Infrastructure, API) -- it was empty before, which meant CI was passing without
building anything; that's fixed. `UnitTests`/`ApiTests` are still empty 0-byte files (not valid
projects), so they're deliberately left out of the solution -- adding them back in once they have
real content is part of `docs/TODO_testing.md`, not this doc.

Hosting is decided and wired up: backend on Render (via the root `Dockerfile`), frontend on
Netlify (via `netlify.toml`). On a push to `main`, once the backend and frontend jobs pass, a
`deploy` job in `cicd.yml` triggers both platforms' deploy hooks -- so CD is done for the
build-and-ship path. What's still open is below.

## What's needed (roughly in order)

1. ~~Decide where this actually runs.~~ Done -- Render (backend) + Netlify (frontend).
2. ~~Containerize.~~ Done -- root `Dockerfile`, multi-stage .NET build.
3. **Database migrations in the deploy pipeline.** Still applied manually via
   `dotnet ef database update` against whatever Postgres instance is targeted -- nothing runs them
   automatically as part of a deploy. Decide how that happens for a real deployment -- e.g. run
   migrations as a startup step, or a separate pipeline step before the app starts, so schema
   changes ship safely instead of relying on someone remembering to run the command by hand.
4. ~~CD: deploy on merge to main.~~ Done -- see "Current state" above.
5. **Health check endpoint.** Still doesn't exist. Most container hosts/load balancers (Render
   included) want a `/health`-style route to confirm the app is up -- worth adding now that there's
   an actual deploy target relying on it.

## Dependencies on other TODOs

This can't be finished without `docs/TODO_production_config.md` (secrets have to come from
somewhere in the deployed environment) being addressed alongside it -- they're two halves of the
same problem. Tests from `docs/TODO_testing.md` aren't a hard blocker for a first deployment, but
CI without any tests to run is much less valuable.
