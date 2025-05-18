FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# It's important to keep lines from here down to "COPY . ." identical in all Dockerfiles
# to take advantage of Docker's build cache, to speed up local container builds
COPY "MediaInAction.sln" "MediaInAction.sln"

# Applications

# Shared
COPY "shared/MediaInAction.Shared.Hosting/MediaInAction.Shared.Hosting.csproj" "shared/MediaInAction.Shared.Hosting/MediaInAction.Shared.Hosting.csproj"
COPY "shared/MediaInAction.Shared.Hosting.AspNetCore/MediaInAction.Shared.Hosting.AspNetCore.csproj" "shared/MediaInAction.Shared.Hosting.AspNetCore/MediaInAction.Shared.Hosting.AspNetCore.csproj"
COPY "shared/MediaInAction.Shared.Hosting.Gateways/MediaInAction.Shared.Hosting.Gateways.csproj" "shared/MediaInAction.Shared.Hosting.Gateways/MediaInAction.Shared.Hosting.Gateways.csproj"
COPY "shared/MediaInAction.Shared.Hosting.Microservices/MediaInAction.Shared.Hosting.Microservices.csproj" "shared/MediaInAction.Shared.Hosting.Microservices/MediaInAction.Shared.Hosting.Microservices.csproj"
COPY "shared/MediaInAction.Shared.Localization/MediaInAction.Shared.Localization.csproj" "shared/MediaInAction.Shared.Localization/MediaInAction.Shared.Localization.csproj"
COPY "shared/MediaInAction.Shared.Domain/MediaInAction.Shared.Domain.csproj" "shared/MediaInAction.Shared.Domain/MediaInAction.Shared.Domain.csproj"
COPY "shared/MediaInAction.Shared.Grpc/MediaInAction.Shared.Grpc.csproj" "shared/MediaInAction.Shared.Grpc/MediaInAction.Shared.Grpc.csproj"
COPY "shared/MediaInAction.Shared.Integration/MediaInAction.Shared.Integration.csproj" "shared/MediaInAction.Shared.Integration/MediaInAction.Shared.Integration.csproj"
COPY "shared/MediaInAction.Keycloak.DbMigrator/MediaInAction.Keycloak.DbMigrator.csproj" "shared/MediaInAction.Keycloak.DbMigrator/MediaInAction.Keycloak.DbMigrator.csproj"

# Microservice
COPY "services/administration/src/MediaInAction.AdministrationService.HttpApi.Host/MediaInAction.AdministrationService.HttpApi.Host.csproj" "services/administration/src/MediaInAction.AdministrationService.HttpApi.Host/MediaInAction.AdministrationService.HttpApi.Host.csproj"
COPY "services/identity/src/MediaInAction.IdentityService.HttpApi.Host/MediaInAction.IdentityService.HttpApi.Host.csproj" "services/identity/src/MediaInAction.IdentityService.HttpApi.Host/MediaInAction.IdentityService.HttpApi.Host.csproj"
COPY "services/emby/src/MediaInAction.EmbyService.HttpApi.Host/MediaInAction.EmbyService.HttpApi.Host.csproj" "services/emby/src/MediaInAction.EmbyService.HttpApi.Host/MediaInAction.EmbyService.HttpApi.Host.csproj"
COPY "services/deluge/src/MediaInAction.DelugeService.HttpApi.Host/MediaInAction.DelugeService.HttpApi.Host.csproj" "services/deluge/src/MediaInAction.DelugeService.HttpApi.Host/MediaInAction.DelugeService.HttpApi.Host.csproj"
COPY "services/video/src/MediaInAction.VideoService.HttpApi.Host/MediaInAction.VideoService.HttpApi.Host.csproj" "services/video/src/MediaInAction.VideoService.HttpApi.Host/MediaInAction.VideoService.HttpApi.Host.csproj"
COPY "services/trakt/src/MediaInAction.TraktService.HttpApi.Host/MediaInAction.TraktService.HttpApi.Host.csproj" "services/trakt/src/MediaInAction.TraktService.HttpApi.Host/MediaInAction.TraktService.HttpApi.Host.csproj"
COPY "services/cmskit/src/MediaInAction.CmskitService.HttpApi.Host/MediaInAction.CmskitService.HttpApi.Host.csproj" "services/cmskit/src/MediaInAction.CmskitService.HttpApi.Host/MediaInAction.CmskitService.HttpApi.Host.csproj"
COPY "services/file/src/MediaInAction.FileService.HttpApi.Host/MediaInAction.FileService.HttpApi.Host.csproj" "services/file/src/MediaInAction.FileService.HttpApi.Host/MediaInAction.FileService.HttpApi.Host.csproj"

# Gateways
COPY "gateways/web/src/MediaInAction.WebGateway/MediaInAction.WebGateway.csproj" "gateways/web/src/MediaInAction.WebGateway/MediaInAction.WebGateway.csproj"
COPY "gateways/web-public/src/MediaInAction.WebPublicGateway/MediaInAction.WebPublicGateway.csproj" "gateways/web-public/src/MediaInAction.WebPublicGateway/MediaInAction.WebPublicGateway.csproj"

COPY "NuGet.Config" "NuGet.Config"

RUN dotnet restore "MediaInAction.sln"
COPY . .


WORKDIR "/src/services/emby/src/MediaInAction.EmbyService.HttpApi.Host"
RUN dotnet publish -c Release -o /app 
# RUN dotnet publish --no-restore -c Release -o /app 

FROM build AS publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MediaInAction.EmbyService.HttpApi.Host.dll"]