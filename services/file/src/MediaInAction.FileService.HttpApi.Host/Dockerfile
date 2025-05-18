FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["services/file/src/MediaInAction.FileService.HttpApi.Host/MediaInAction.FileService.HttpApi.Host.csproj", "services/file/src/MediaInAction.FileService.HttpApi.Host/"]
COPY ["shared/MediaInAction.Shared.Hosting.Microservices/MediaInAction.Shared.Hosting.Microservices.csproj", "shared/MediaInAction.Shared.Hosting.Microservices/"]
COPY ["shared/MediaInAction.Shared.Hosting.AspNetCore/MediaInAction.Shared.Hosting.AspNetCore.csproj", "shared/MediaInAction.Shared.Hosting.AspNetCore/"]
COPY ["aspire/ServiceDefaults/MediaInAction.ServiceDefaults.csproj", "aspire/ServiceDefaults/"]
COPY ["shared/MediaInAction.Shared.Hosting/MediaInAction.Shared.Hosting.csproj", "shared/MediaInAction.Shared.Hosting/"]
COPY ["shared/MediaInAction.Shared.Localization/MediaInAction.Shared.Localization.csproj", "shared/MediaInAction.Shared.Localization/"]
COPY ["services/administration/src/MediaInAction.AdministrationService.EntityFrameworkCore/MediaInAction.AdministrationService.EntityFrameworkCore.csproj", "services/administration/src/MediaInAction.AdministrationService.EntityFrameworkCore/"]
COPY ["services/administration/src/MediaInAction.AdministrationService.Domain/MediaInAction.AdministrationService.Domain.csproj", "services/administration/src/MediaInAction.AdministrationService.Domain/"]
COPY ["services/administration/src/MediaInAction.AdministrationService.Domain.Shared/MediaInAction.AdministrationService.Domain.Shared.csproj", "services/administration/src/MediaInAction.AdministrationService.Domain.Shared/"]
COPY ["services/file/src/MediaInAction.FileService.Application/MediaInAction.FileService.Application.csproj", "services/file/src/MediaInAction.FileService.Application/"]
COPY ["services/file/src/MediaInAction.FileService.Domain/MediaInAction.FileService.Domain.csproj", "services/file/src/MediaInAction.FileService.Domain/"]
COPY ["shared/MediaInAction.Shared.Domain/MediaInAction.Shared.Domain.csproj", "shared/MediaInAction.Shared.Domain/"]
COPY ["services/file/src/MediaInAction.FileService.Domain.Shared/MediaInAction.FileService.Domain.Shared.csproj", "services/file/src/MediaInAction.FileService.Domain.Shared/"]
COPY ["services/video/src/MediaInAction.VideoService.Domain.Shared/MediaInAction.VideoService.Domain.Shared.csproj", "services/video/src/MediaInAction.VideoService.Domain.Shared/"]
COPY ["services/file/src/MediaInAction.FileService.Application.Contracts/MediaInAction.FileService.Application.Contracts.csproj", "services/file/src/MediaInAction.FileService.Application.Contracts/"]
COPY ["services/file/src/MediaInAction.FileService.MongoDb/MediaInAction.FileService.MongoDb.csproj", "services/file/src/MediaInAction.FileService.MongoDb/"]
COPY ["services/file/src/MediaInAction.FileService.HttpApi/MediaInAction.FileService.HttpApi.csproj", "services/file/src/MediaInAction.FileService.HttpApi/"]
RUN dotnet restore "services/file/src/MediaInAction.FileService.HttpApi.Host/MediaInAction.FileService.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/services/file/src/MediaInAction.FileService.HttpApi.Host"
RUN dotnet build "MediaInAction.FileService.HttpApi.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MediaInAction.FileService.HttpApi.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MediaInAction.FileService.HttpApi.Host.dll"]
