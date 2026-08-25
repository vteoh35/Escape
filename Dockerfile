# Multi-stage build for the ASP.NET Core API. Build context is the repo root (so it can reach
# both backend/ projects); Render should be configured with Root Directory = repo root and
# Dockerfile Path = Dockerfile.

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Restore first, copying only the .csproj files, so Docker can cache the restore layer across
# builds that only change source code. "Business Logic" has a space in the folder name -- the
# bracketed COPY syntax handles that safely.
COPY ["backend/src/API/Api.csproj", "backend/src/API/"]
COPY ["backend/src/Application/Application.csproj", "backend/src/Application/"]
COPY ["backend/src/Infrastructure/Infrastructure.csproj", "backend/src/Infrastructure/"]
COPY ["backend/src/Business Logic/Business_Logic.csproj", "backend/src/Business Logic/"]
RUN dotnet restore "backend/src/API/Api.csproj"

COPY backend/src/ backend/src/

WORKDIR "/src/backend/src/API"
RUN dotnet publish "Api.csproj" -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_ENVIRONMENT=Production
# Render's containers have a very low inotify limit; ASP.NET Core's default JSON config
# file-watcher uses inotify and crashes the app on startup if this isn't disabled.
ENV DOTNET_hostBuilder__reloadConfigOnChange=false
EXPOSE 8080

# Render injects $PORT at runtime; fall back to 8080 for local `docker run` testing. Shell-form
# CMD is required here so $PORT actually gets expanded.
CMD ASPNETCORE_URLS=http://+:${PORT:-8080} dotnet Api.dll
