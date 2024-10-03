FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["shared/MediaInAction.Keycloak.DbMigrator/MediaInAction.Keycloak.DbMigrator.csproj", "shared/MediaInAction.Keycloak.DbMigrator/"]
COPY ["shared/MediaInAction.Shared.Hosting/MediaInAction.Shared.Hosting.csproj", "shared/MediaInAction.Shared.Hosting/"]
RUN dotnet restore "shared/MediaInAction.Keycloak.DbMigrator/MediaInAction.Keycloak.DbMigrator.csproj"
COPY . .
WORKDIR "/shared/MediaInAction.Keycloak.DbMigrator"
RUN dotnet build "MediaInAction.Keycloak.DbMigrator.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MediaInAction.Keycloak.DbMigrator.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MediaInAction.Keycloak.DbMigrator.dll"]
