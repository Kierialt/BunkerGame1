# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files
COPY BunkerGame.sln .
COPY BunkerGame.Backend/BunkerGame.Backend.csproj BunkerGame.Backend/
COPY BunkerGame.Frontend/BunkerGame.Frontend.csproj BunkerGame.Frontend/

# Restore
RUN dotnet restore BunkerGame.Backend/BunkerGame.Backend.csproj

# Copy backend and frontend source
COPY BunkerGame.Backend/ BunkerGame.Backend/
COPY BunkerGame.Frontend/ BunkerGame.Frontend/

# Publish backend
WORKDIR /src/BunkerGame.Backend
RUN dotnet publish -c Release -o /app/publish --no-restore

# Copy frontend wwwroot into publish wwwroot (single-server: backend serves static files)
RUN mkdir -p /app/publish/wwwroot && cp -r /src/BunkerGame.Frontend/wwwroot/. /app/publish/wwwroot/

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Render sets PORT at runtime (default 10000)
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://0.0.0.0:8080

COPY --from=build /app/publish .

# Use PORT from environment when set (Render)
ENTRYPOINT ["/bin/sh", "-c", "export ASPNETCORE_URLS=http://0.0.0.0:${PORT:-8080} && exec dotnet BunkerGame.Backend.dll"]
