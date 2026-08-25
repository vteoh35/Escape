# TODO: Production configuration

Read `docs/PROJECT_STATUS.md` first for overall context if you haven't.

## Current state

Only local development configuration exists:
- `src/API/appsettings.json` (committed) has an empty `ConnectionStrings:DefaultConnection`.
- `src/API/appsettings.Development.json` (gitignored, exists locally) has the real dev Postgres
  connection string (with a plaintext password) and a `Jwt:Key` value explicitly named
  `"dev-only-signing-key-do-not-use-in-production..."` -- it says what it is.
- CORS is hardcoded in `program.cs` to `http://localhost:4200` only (the Angular dev server).
- No secrets manager, no environment-variable-based config, no staging/production appsettings file.

If you deploy this as-is to anything other than a developer's own machine, it will not start
(`Jwt:Key` throws `InvalidOperationException` if missing, connection string is empty) -- which is
actually the correct fail-fast behavior, not a bug. The work here is providing real values through
a real mechanism, not removing the checks.

## What's needed

1. **Connection string** for the real target database (staging/production Postgres instance),
   supplied via environment variable or a secrets manager -- never committed to
   `appsettings.json`. ASP.NET Core's configuration system reads env vars automatically
   (`ConnectionStrings__DefaultConnection`), so this may not need code changes, just deployment-time
   configuration.
2. **`Jwt:Key`** -- a real, sufficiently random signing key (not the dev placeholder), same
   env-var/secrets-manager approach (`Jwt__Key`). Rotating this invalidates all issued tokens, so
   treat it as a real secret, not something to regenerate casually.
3. **CORS** -- add the real frontend origin(s) once the frontend has a deployed URL. Don't just
   open CORS to `*` -- keep it to an explicit allowlist, same pattern as the current `"Frontend"`
   policy in `program.cs`.
4. **`appsettings.Production.json`** (or environment-specific files as needed) for non-secret
   config that differs by environment -- logging levels, etc. Secrets still don't belong in these
   files even for Production; they're committed to source control just like `appsettings.json`.
5. Decide on a secrets mechanism appropriate for wherever this actually gets hosted (see
   `docs/TODO_deployment.md` -- the two are linked: e.g. Azure App Service -> Key Vault + App
   Settings, a Docker/VM deployment -> environment variables injected at container/VM level, etc.)
   This doc intentionally doesn't prescribe a specific cloud provider since that hasn't been decided
   yet.

## Verification

Before considering this done: confirm the app actually starts with production-shaped config (real
env vars set, no dev appsettings file present) and that it fails clearly (not silently) if a
required secret is missing -- the existing `Jwt:Key` throw is the pattern to replicate for anything
else that's genuinely required to run.
