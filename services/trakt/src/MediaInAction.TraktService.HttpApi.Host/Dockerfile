FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["services/trakt/src/MediaInAction.TraktService.HttpApi.Host/MediaInAction.TraktService.HttpApi.Host.csproj", "services/trakt/src/MediaInAction.TraktService.HttpApi.Host/"]
COPY ["aspire/ServiceDefaults/MediaInAction.ServiceDefaults.csproj", "aspire/ServiceDefaults/"]
COPY ["services/video/src/MediaInAction.VideoService.Application/MediaInAction.VideoService.Application.csproj", "services/video/src/MediaInAction.VideoService.Application/"]
COPY ["services/video/src/MediaInAction.VideoService.Application.Contracts/MediaInAction.VideoService.Application.Contracts.csproj", "services/video/src/MediaInAction.VideoService.Application.Contracts/"]
COPY ["services/video/src/MediaInAction.VideoService.Domain.Shared/MediaInAction.VideoService.Domain.Shared.csproj", "services/video/src/MediaInAction.VideoService.Domain.Shared/"]
COPY ["shared/MediaInAction.Shared.Domain/MediaInAction.Shared.Domain.csproj", "shared/MediaInAction.Shared.Domain/"]
COPY ["services/video/src/MediaInAction.VideoService.Domain/MediaInAction.VideoService.Domain.csproj", "services/video/src/MediaInAction.VideoService.Domain/"]
COPY ["services/trakt/src/MediaInAction.TraktService.Application/MediaInAction.TraktService.Application.csproj", "services/trakt/src/MediaInAction.TraktService.Application/"]
COPY ["services/trakt/src/MediaInAction.TraktService.Domain/MediaInAction.TraktService.Domain.csproj", "services/trakt/src/MediaInAction.TraktService.Domain/"]
COPY ["services/trakt/src/MediaInAction.TraktService.Domain.Shared/MediaInAction.TraktService.Domain.Shared.csproj", "services/trakt/src/MediaInAction.TraktService.Domain.Shared/"]
COPY ["services/trakt/src/MediaInAction.TraktService.Application.Contracts/MediaInAction.TraktService.Application.Contracts.csproj", "services/trakt/src/MediaInAction.TraktService.Application.Contracts/"]
COPY ["services/trakt/src/MediaInAction.TraktService.Bg.Workers/MediaInAction.TraktService.Bg.Workers.csproj", "services/trakt/src/MediaInAction.TraktService.Bg.Workers/"]
COPY ["services/trakt/src/MediaInAction.TraktService.Lib/MediaInAction.TraktService.Lib.csproj", "services/trakt/src/MediaInAction.TraktService.Lib/"]
COPY ["services/trakt/src/MediaInAction.TraktService.MongoDB/MediaInAction.TraktService.MongoDB.csproj", "services/trakt/src/MediaInAction.TraktService.MongoDB/"]
COPY ["services/trakt/src/MediaInAction.TraktService.HttpApi/MediaInAction.TraktService.HttpApi.csproj", "services/trakt/src/MediaInAction.TraktService.HttpApi/"]
COPY ["shared/MediaInAction.Shared.Hosting.Microservices/MediaInAction.Shared.Hosting.Microservices.csproj", "shared/MediaInAction.Shared.Hosting.Microservices/"]
COPY ["shared/MediaInAction.Shared.Hosting.AspNetCore/MediaInAction.Shared.Hosting.AspNetCore.csproj", "shared/MediaInAction.Shared.Hosting.AspNetCore/"]
COPY ["shared/MediaInAction.Shared.Hosting/MediaInAction.Shared.Hosting.csproj", "shared/MediaInAction.Shared.Hosting/"]
COPY ["shared/MediaInAction.Shared.Localization/MediaInAction.Shared.Localization.csproj", "shared/MediaInAction.Shared.Localization/"]
COPY ["services/administration/src/MediaInAction.AdministrationService.EntityFrameworkCore/MediaInAction.AdministrationService.EntityFrameworkCore.csproj", "services/administration/src/MediaInAction.AdministrationService.EntityFrameworkCore/"]
COPY ["services/administration/src/MediaInAction.AdministrationService.Domain/MediaInAction.AdministrationService.Domain.csproj", "services/administration/src/MediaInAction.AdministrationService.Domain/"]
COPY ["services/administration/src/MediaInAction.AdministrationService.Domain.Shared/MediaInAction.AdministrationService.Domain.Shared.csproj", "services/administration/src/MediaInAction.AdministrationService.Domain.Shared/"]
RUN dotnet restore "services/trakt/src/MediaInAction.TraktService.HttpApi.Host/MediaInAction.TraktService.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/services/trakt/src/MediaInAction.TraktService.HttpApi.Host"
RUN dotnet build "MediaInAction.TraktService.HttpApi.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MediaInAction.TraktService.HttpApi.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MediaInAction.TraktService.HttpApi.Host.dll"]
