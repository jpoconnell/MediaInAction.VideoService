FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["services/administration/src/MediaInAction.AdministrationService.HttpApi.Host/MediaInAction.AdministrationService.HttpApi.Host.csproj", "services/administration/src/MediaInAction.AdministrationService.HttpApi.Host/"]
COPY ["aspire/ServiceDefaults/MediaInAction.ServiceDefaults.csproj", "aspire/ServiceDefaults/"]
COPY ["services/administration/src/MediaInAction.AdministrationService.EntityFrameworkCore/MediaInAction.AdministrationService.EntityFrameworkCore.csproj", "services/administration/src/MediaInAction.AdministrationService.EntityFrameworkCore/"]
COPY ["services/administration/src/MediaInAction.AdministrationService.Domain/MediaInAction.AdministrationService.Domain.csproj", "services/administration/src/MediaInAction.AdministrationService.Domain/"]
COPY ["services/administration/src/MediaInAction.AdministrationService.Domain.Shared/MediaInAction.AdministrationService.Domain.Shared.csproj", "services/administration/src/MediaInAction.AdministrationService.Domain.Shared/"]
COPY ["services/administration/src/MediaInAction.AdministrationService.Application/MediaInAction.AdministrationService.Application.csproj", "services/administration/src/MediaInAction.AdministrationService.Application/"]
COPY ["services/administration/src/MediaInAction.AdministrationService.Application.Contracts/MediaInAction.AdministrationService.Application.Contracts.csproj", "services/administration/src/MediaInAction.AdministrationService.Application.Contracts/"]
COPY ["services/administration/src/MediaInAction.AdministrationService.HttpApi/MediaInAction.AdministrationService.HttpApi.csproj", "services/administration/src/MediaInAction.AdministrationService.HttpApi/"]
COPY ["shared/MediaInAction.Shared.Hosting.Microservices/MediaInAction.Shared.Hosting.Microservices.csproj", "shared/MediaInAction.Shared.Hosting.Microservices/"]
COPY ["shared/MediaInAction.Shared.Hosting.AspNetCore/MediaInAction.Shared.Hosting.AspNetCore.csproj", "shared/MediaInAction.Shared.Hosting.AspNetCore/"]
COPY ["shared/MediaInAction.Shared.Hosting/MediaInAction.Shared.Hosting.csproj", "shared/MediaInAction.Shared.Hosting/"]
COPY ["shared/MediaInAction.Shared.Localization/MediaInAction.Shared.Localization.csproj", "shared/MediaInAction.Shared.Localization/"]
RUN dotnet restore "services/administration/src/MediaInAction.AdministrationService.HttpApi.Host/MediaInAction.AdministrationService.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/services/administration/src/MediaInAction.AdministrationService.HttpApi.Host"
RUN dotnet build "MediaInAction.AdministrationService.HttpApi.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MediaInAction.AdministrationService.HttpApi.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MediaInAction.AdministrationService.HttpApi.Host.dll"]
