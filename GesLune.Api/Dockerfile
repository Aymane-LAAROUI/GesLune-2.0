# ---------- Runtime image ----------
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

# ---------- Build image ----------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj files FIRST (for restore caching)
COPY GesLune.Api/GesLune.Api.csproj GesLune.Api/
COPY GesLune/GesLune.csproj GesLune/

# Restore only the API project
RUN dotnet restore GesLune.Api/GesLune.Api.csproj

# Copy the rest of the repo
COPY . .

# Publish the API project
RUN dotnet publish GesLune.Api/GesLune.Api.csproj \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

# ---------- Final image ----------
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "GesLune.Api.dll"]
