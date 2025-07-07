# SDK stage
FROM mcr.microsoft.com/dotnet/sdk:9.0-preview AS build
WORKDIR /app

COPY ../clean.sln .
COPY ../clean.api/ ./clean.api/
COPY ../clean.domain/ ./clean.domain/
COPY ../clean.application/ ./clean.application/
COPY ../clean.infrastructure/ ./clean.infrastructure/
COPY ../clean.persistence/ ./clean.persistence/

WORKDIR /app/clean.api
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0-preview
WORKDIR /app
COPY --from=build /app/clean.api/out .
ENTRYPOINT ["dotnet", "clean.api.dll"]
