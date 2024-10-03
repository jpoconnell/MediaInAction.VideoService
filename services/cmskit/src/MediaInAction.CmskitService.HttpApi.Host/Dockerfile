FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 81

# add globalization support
RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

# add diagnostic tools
RUN apk add --no-cache curl

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /work

# Start build cache
COPY "MediaInAction.sln" "MediaInAction.sln"

WORKDIR /work/src
# frontends
COPY "apps/public-web/src/MediaInAction.TraktService.Web/MediaInAction.TraktService.Web.csproj" "apps/public-web/src/MediaInAction.TraktService.Web/MediaInAction.TraktService.Web.csproj"
COPY "apps/public-web/src/MediaInAction.PublicWeb.Web/MediaInAction.PublicWeb.Web.csproj" "apps/public-web/src/MediaInAction.PublicWeb.Web/MediaInAction.PublicWeb.Web.csproj"
COPY "apps/public-web/src/MediaInAction.PublicWeb/MediaInAction.PublicWeb.csproj" "apps/public-web/src/MediaInAction.PublicWeb/MediaInAction.PublicWeb.csproj"

COPY "gateways/web/src/MediaInAction.WebGateway/MediaInAction.WebGateway.csproj" "gateways/web/src/MediaInAction.WebGateway/MediaInAction.WebGateway.csproj"

COPY "gateways/web-public/src/MediaInAction.WebPublicGateway/MediaInAction.WebPublicGateway.csproj" "gateways/web-public/src/MediaInAction.WebPublicGateway/MediaInAction.WebPublicGateway.csproj"

# administration
COPY "services/administration/src/MediaInAction.AdministrationService.HttpApi/MediaInAction.AdministrationService.HttpApi.csproj" "services/administration/src/MediaInAction.AdministrationService.HttpApi/MediaInAction.AdministrationService.HttpApi.csproj"
COPY "services/administration/src/MediaInAction.AdministrationService.HttpApi.Client/MediaInAction.AdministrationService.HttpApi.Client.csproj" "services/administration/src/MediaInAction.AdministrationService.HttpApi.Client/MediaInAction.AdministrationService.HttpApi.Client.csproj"
COPY "services/administration/src/MediaInAction.AdministrationService.Domain.Shared/MediaInAction.AdministrationService.Domain.Shared.csproj" "services/administration/src/MediaInAction.AdministrationService.Domain.Shared/MediaInAction.AdministrationService.Domain.Shared.csproj"
COPY "services/administration/src/MediaInAction.AdministrationService.EntityFrameworkCore/MediaInAction.AdministrationService.EntityFrameworkCore.csproj" "services/administration/src/MediaInAction.AdministrationService.EntityFrameworkCore/MediaInAction.AdministrationService.EntityFrameworkCore.csproj"
COPY "services/administration/src/MediaInAction.AdministrationService.Application/MediaInAction.AdministrationService.Application.csproj" "services/administration/src/MediaInAction.AdministrationService.Application/MediaInAction.AdministrationService.Application.csproj"
COPY "services/administration/src/MediaInAction.AdministrationService.Application.Contracts/MediaInAction.AdministrationService.Application.Contracts.csproj" "services/administration/src/MediaInAction.AdministrationService.Application.Contracts/MediaInAction.AdministrationService.Application.Contracts.csproj"
COPY "services/administration/src/MediaInAction.AdministrationService.Domain/MediaInAction.AdministrationService.Domain.csproj" "services/administration/src/MediaInAction.AdministrationService.Domain/MediaInAction.AdministrationService.Domain.csproj"
COPY "services/administration/src/MediaInAction.AdministrationService.HttpApi.Host/MediaInAction.AdministrationService.HttpApi.Host.csproj" "services/administration/src/MediaInAction.AdministrationService.HttpApi.Host/MediaInAction.AdministrationService.HttpApi.Host.csproj"
COPY "services/administration/test/MediaInAction.AdministrationService.EntityFrameworkCore.Tests/MediaInAction.AdministrationService.EntityFrameworkCore.Tests.csproj" "services/administration/test/MediaInAction.AdministrationService.EntityFrameworkCore.Tests/MediaInAction.AdministrationService.EntityFrameworkCore.Tests.csproj"
COPY "services/administration/test/MediaInAction.AdministrationService.Domain.Tests/MediaInAction.AdministrationService.Domain.Tests.csproj" "services/administration/test/MediaInAction.AdministrationService.Domain.Tests/MediaInAction.AdministrationService.Domain.Tests.csproj"
COPY "services/administration/test/MediaInAction.AdministrationService.Application.Tests/MediaInAction.AdministrationService.Application.Tests.csproj" "services/administration/test/MediaInAction.AdministrationService.Application.Tests/MediaInAction.AdministrationService.Application.Tests.csproj"
COPY "services/administration/test/MediaInAction.AdministrationService.TestBase/MediaInAction.AdministrationService.TestBase.csproj" "services/administration/test/MediaInAction.AdministrationService.TestBase/MediaInAction.AdministrationService.TestBase.csproj"

# identity
COPY "services/identity/src/MediaInAction.IdentityService.Application.Contracts/MediaInAction.IdentityService.Application.Contracts.csproj" "services/identity/src/MediaInAction.IdentityService.Application.Contracts/MediaInAction.IdentityService.Application.Contracts.csproj"
COPY "services/identity/src/MediaInAction.IdentityService.Application/MediaInAction.IdentityService.Application.csproj" "services/identity/src/MediaInAction.IdentityService.Application/MediaInAction.IdentityService.Application.csproj"
COPY "services/identity/src/MediaInAction.IdentityService.Domain/MediaInAction.IdentityService.Domain.csproj" "services/identity/src/MediaInAction.IdentityService.Domain/MediaInAction.IdentityService.Domain.csproj"
COPY "services/identity/src/MediaInAction.IdentityService.EntityFrameworkCore/MediaInAction.IdentityService.EntityFrameworkCore.csproj" "services/identity/src/MediaInAction.IdentityService.EntityFrameworkCore/MediaInAction.IdentityService.EntityFrameworkCore.csproj"
COPY "services/identity/src/MediaInAction.IdentityService.HttpApi/MediaInAction.IdentityService.HttpApi.csproj" "services/identity/src/MediaInAction.IdentityService.HttpApi/MediaInAction.IdentityService.HttpApi.csproj"
COPY "services/identity/src/MediaInAction.IdentityService.Domain.Shared/MediaInAction.IdentityService.Domain.Shared.csproj" "services/identity/src/MediaInAction.IdentityService.Domain.Shared/MediaInAction.IdentityService.Domain.Shared.csproj"
COPY "services/identity/src/MediaInAction.IdentityService.HttpApi.Host/MediaInAction.IdentityService.HttpApi.Host.csproj" "services/identity/src/MediaInAction.IdentityService.HttpApi.Host/MediaInAction.IdentityService.HttpApi.Host.csproj"
COPY "services/identity/src/MediaInAction.IdentityService.HttpApi.Client/MediaInAction.IdentityService.HttpApi.Client.csproj" "services/identity/src/MediaInAction.IdentityService.HttpApi.Client/MediaInAction.IdentityService.HttpApi.Client.csproj"
COPY "services/identity/test/MediaInAction.IdentityService.EntityFrameworkCore.Tests/MediaInAction.IdentityService.EntityFrameworkCore.Tests.csproj" "services/identity/test/MediaInAction.IdentityService.EntityFrameworkCore.Tests/MediaInAction.IdentityService.EntityFrameworkCore.Tests.csproj"
COPY "services/identity/test/MediaInAction.IdentityService.Application.Tests/MediaInAction.IdentityService.Application.Tests.csproj" "services/identity/test/MediaInAction.IdentityService.Application.Tests/MediaInAction.IdentityService.Application.Tests.csproj"
COPY "services/identity/test/MediaInAction.IdentityService.TestBase/MediaInAction.IdentityService.TestBase.csproj" "services/identity/test/MediaInAction.IdentityService.TestBase/MediaInAction.IdentityService.TestBase.csproj"
COPY "services/identity/test/MediaInAction.IdentityService.Domain.Tests/MediaInAction.IdentityService.Domain.Tests.csproj" "services/identity/test/MediaInAction.IdentityService.Domain.Tests/MediaInAction.IdentityService.Domain.Tests.csproj"

# emby
COPY "services/emby/src/MediaInAction.EmbyService.Domain/MediaInAction.EmbyService.Domain.csproj" "services/emby/src/MediaInAction.EmbyService.Domain/MediaInAction.EmbyService.Domain.csproj"
COPY "services/emby/src/MediaInAction.EmbyService.Lib/MediaInAction.EmbyService.Lib.csproj" "services/emby/src/MediaInAction.EmbyService.Lib/MediaInAction.EmbyService.Lib.csproj"
COPY "services/emby/src/MediaInAction.EmbyService.HttpApi.Client/MediaInAction.EmbyService.HttpApi.Client.csproj" "services/emby/src/MediaInAction.EmbyService.HttpApi.Client/MediaInAction.EmbyService.HttpApi.Client.csproj"
COPY "services/emby/src/MediaInAction.EmbyService.MongoDb/MediaInAction.EmbyService.MongoDb.csproj" "services/emby/src/MediaInAction.EmbyService.MongoDb/MediaInAction.EmbyService.MongoDb.csproj"
COPY "services/emby/src/MediaInAction.EmbyService.HttpApi/MediaInAction.EmbyService.HttpApi.csproj" "services/emby/src/MediaInAction.EmbyService.HttpApi/MediaInAction.EmbyService.HttpApi.csproj"
COPY "services/emby/src/MediaInAction.EmbyService.HttpApi.Host/MediaInAction.EmbyService.HttpApi.Host.csproj" "services/emby/src/MediaInAction.EmbyService.HttpApi.Host/MediaInAction.EmbyService.HttpApi.Host.csproj"
COPY "services/emby/src/MediaInAction.EmbyService.Domain.Shared/MediaInAction.EmbyService.Domain.Shared.csproj" "services/emby/src/MediaInAction.EmbyService.Domain.Shared/MediaInAction.EmbyService.Domain.Shared.csproj"
COPY "services/emby/src/MediaInAction.EmbyService.Bg.Workers/MediaInAction.EmbyService.Bg.Workers.csproj" "services/emby/src/MediaInAction.EmbyService.Bg.Workers/MediaInAction.EmbyService.Bg.Workers.csproj"
COPY "services/emby/src/MediaInAction.EmbyService.Application/MediaInAction.EmbyService.Application.csproj" "services/emby/src/MediaInAction.EmbyService.Application/MediaInAction.EmbyService.Application.csproj"
COPY "services/emby/src/MediaInAction.EmbyService.Bg.Jobs/MediaInAction.EmbyService.Bg.Jobs.csproj" "services/emby/src/MediaInAction.EmbyService.Bg.Jobs/MediaInAction.EmbyService.Bg.Jobs.csproj"
COPY "services/emby/src/MediaInAction.EmbyService.Application.Contracts/MediaInAction.EmbyService.Application.Contracts.csproj" "services/emby/src/MediaInAction.EmbyService.Application.Contracts/MediaInAction.EmbyService.Application.Contracts.csproj"
COPY "services/emby/test/MediaInAction.EmbyService.MongoDb.Tests/MediaInAction.EmbyService.MongoDb.Tests.csproj" "services/emby/test/MediaInAction.EmbyService.MongoDb.Tests/MediaInAction.EmbyService.MongoDb.Tests.csproj"
COPY "services/emby/test/MediaInAction.EmbyService.Domain.Tests/MediaInAction.EmbyService.Domain.Tests.csproj" "services/emby/test/MediaInAction.EmbyService.Domain.Tests/MediaInAction.EmbyService.Domain.Tests.csproj"
COPY "services/emby/test/MediaInAction.EmbyService.Application.Tests/MediaInAction.EmbyService.Application.Tests.csproj" "services/emby/test/MediaInAction.EmbyService.Application.Tests/MediaInAction.EmbyService.Application.Tests.csproj"
COPY "services/emby/test/MediaInAction.EmbyService.HttpApi.Client.ConsoleTestApp/MediaInAction.EmbyService.HttpApi.Client.ConsoleTestApp.csproj" "services/emby/test/MediaInAction.EmbyService.HttpApi.Client.ConsoleTestApp/MediaInAction.EmbyService.HttpApi.Client.ConsoleTestApp.csproj"
COPY "services/emby/test/MediaInAction.EmbyService.TestBase/MediaInAction.EmbyService.TestBase.csproj" "services/emby/test/MediaInAction.EmbyService.TestBase/MediaInAction.EmbyService.TestBase.csproj"

# deluge
COPY "services/deluge/src/MediaInAction.DelugeService.BG.Workers/MediaInAction.DelugeService.BG.Workers.csproj" "services/deluge/src/MediaInAction.DelugeService.BG.Workers/MediaInAction.DelugeService.BG.Workers.csproj"
COPY "services/deluge/src/MediaInAction.DelugeService.MongoDb/MediaInAction.DelugeService.MongoDb.csproj" "services/deluge/src/MediaInAction.DelugeService.MongoDb/MediaInAction.DelugeService.MongoDb.csproj"
COPY "services/deluge/src/MediaInAction.DelugeService.HttpApi/MediaInAction.DelugeService.HttpApi.csproj" "services/deluge/src/MediaInAction.DelugeService.HttpApi/MediaInAction.DelugeService.HttpApi.csproj"
COPY "services/deluge/src/MediaInAction.DelugeService.Application/MediaInAction.DelugeService.Application.csproj" "services/deluge/src/MediaInAction.DelugeService.Application/MediaInAction.DelugeService.Application.csproj"
COPY "services/deluge/src/MediaInAction.DelugeService.HttpApi.Client/MediaInAction.DelugeService.HttpApi.Client.csproj" "services/deluge/src/MediaInAction.DelugeService.HttpApi.Client/MediaInAction.DelugeService.HttpApi.Client.csproj"
COPY "services/deluge/src/MediaInAction.DelugeService.HttpApi.Host/MediaInAction.DelugeService.HttpApi.Host.csproj" "services/deluge/src/MediaInAction.DelugeService.HttpApi.Host/MediaInAction.DelugeService.HttpApi.Host.csproj"
COPY "services/deluge/src/MediaInAction.DelugeService.Lib/MediaInAction.DelugeService.Lib.csproj" "services/deluge/src/MediaInAction.DelugeService.Lib/MediaInAction.DelugeService.Lib.csproj"
COPY "services/deluge/src/MediaInAction.DelugeService.Domain.Shared/MediaInAction.DelugeService.Domain.Shared.csproj" "services/deluge/src/MediaInAction.DelugeService.Domain.Shared/MediaInAction.DelugeService.Domain.Shared.csproj"
COPY "services/deluge/src/MediaInAction.DelugeService.Application.Contracts/MediaInAction.DelugeService.Application.Contracts.csproj" "services/deluge/src/MediaInAction.DelugeService.Application.Contracts/MediaInAction.DelugeService.Application.Contracts.csproj"
COPY "services/deluge/src/MediaInAction.DelugeService.Domain/MediaInAction.DelugeService.Domain.csproj" "services/deluge/src/MediaInAction.DelugeService.Domain/MediaInAction.DelugeService.Domain.csproj"
COPY "services/deluge/src/MediaInAction.DelugeService.BG.Jobs/MediaInAction.DelugeService.BG.Jobs.csproj" "services/deluge/src/MediaInAction.DelugeService.BG.Jobs/MediaInAction.DelugeService.BG.Jobs.csproj"
COPY "services/deluge/test/MediaInAction.DelugeService.TestBase/MediaInAction.DelugeService.TestBase.csproj" "services/deluge/test/MediaInAction.DelugeService.TestBase/MediaInAction.DelugeService.TestBase.csproj"
COPY "services/deluge/test/MediaInAction.DelugeService.MongoDb.Tests/MediaInAction.DelugeService.MongoDb.Tests.csproj" "services/deluge/test/MediaInAction.DelugeService.MongoDb.Tests/MediaInAction.DelugeService.MongoDb.Tests.csproj"
COPY "services/deluge/test/MediaInAction.DelugeService.Application.Tests/MediaInAction.DelugeService.Application.Tests.csproj" "services/deluge/test/MediaInAction.DelugeService.Application.Tests/MediaInAction.DelugeService.Application.Tests.csproj"
COPY "services/deluge/test/MediaInAction.DelugeService.Domain.Tests/MediaInAction.DelugeService.Domain.Tests.csproj" "services/deluge/test/MediaInAction.DelugeService.Domain.Tests/MediaInAction.DelugeService.Domain.Tests.csproj"

# cmskit
COPY "services/cmskit/src/MediaInAction.CmskitService.HttpApi.Host/MediaInAction.CmskitService.HttpApi.Host.csproj" "services/cmskit/src/MediaInAction.CmskitService.HttpApi.Host/MediaInAction.CmskitService.HttpApi.Host.csproj"
COPY "services/cmskit/src/MediaInAction.CmskitService.Domain/MediaInAction.CmskitService.Domain.csproj" "services/cmskit/src/MediaInAction.CmskitService.Domain/MediaInAction.CmskitService.Domain.csproj"
COPY "services/cmskit/src/MediaInAction.CmskitService.Application/MediaInAction.CmskitService.Application.csproj" "services/cmskit/src/MediaInAction.CmskitService.Application/MediaInAction.CmskitService.Application.csproj"
COPY "services/cmskit/src/MediaInAction.CmskitService.EntityFrameworkCore/MediaInAction.CmskitService.EntityFrameworkCore.csproj" "services/cmskit/src/MediaInAction.CmskitService.EntityFrameworkCore/MediaInAction.CmskitService.EntityFrameworkCore.csproj"
COPY "services/cmskit/src/MediaInAction.CmskitService.Application.Contracts/MediaInAction.CmskitService.Application.Contracts.csproj" "services/cmskit/src/MediaInAction.CmskitService.Application.Contracts/MediaInAction.CmskitService.Application.Contracts.csproj"
COPY "services/cmskit/src/MediaInAction.CmskitService.HttpApi/MediaInAction.CmskitService.HttpApi.csproj" "services/cmskit/src/MediaInAction.CmskitService.HttpApi/MediaInAction.CmskitService.HttpApi.csproj"
COPY "services/cmskit/src/MediaInAction.CmskitService.Domain.Shared/MediaInAction.CmskitService.Domain.Shared.csproj" "services/cmskit/src/MediaInAction.CmskitService.Domain.Shared/MediaInAction.CmskitService.Domain.Shared.csproj"
COPY "services/cmskit/src/MediaInAction.CmskitService.HttpApi.Client/MediaInAction.CmskitService.HttpApi.Client.csproj" "services/cmskit/src/MediaInAction.CmskitService.HttpApi.Client/MediaInAction.CmskitService.HttpApi.Client.csproj"
COPY "services/cmskit/test/MediaInAction.CmskitService.Domain.Tests/MediaInAction.CmskitService.Domain.Tests.csproj" "services/cmskit/test/MediaInAction.CmskitService.Domain.Tests/MediaInAction.CmskitService.Domain.Tests.csproj"
COPY "services/cmskit/test/MediaInAction.CmskitService.TestBase/MediaInAction.CmskitService.TestBase.csproj" "services/cmskit/test/MediaInAction.CmskitService.TestBase/MediaInAction.CmskitService.TestBase.csproj"
COPY "services/cmskit/test/MediaInAction.CmskitService.Application.Tests/MediaInAction.CmskitService.Application.Tests.csproj" "services/cmskit/test/MediaInAction.CmskitService.Application.Tests/MediaInAction.CmskitService.Application.Tests.csproj"
COPY "services/cmskit/test/MediaInAction.CmskitService.EntityFrameworkCore.Tests/MediaInAction.CmskitService.EntityFrameworkCore.Tests.csproj" "services/cmskit/test/MediaInAction.CmskitService.EntityFrameworkCore.Tests/MediaInAction.CmskitService.EntityFrameworkCore.Tests.csproj"

# file
COPY "services/file/src/MediaInAction.FileService.BG.Workers/MediaInAction.FileService.BG.Workers.csproj" "services/file/src/MediaInAction.FileService.BG.Workers/MediaInAction.FileService.BG.Workers.csproj"
COPY "services/file/src/MediaInAction.FileService.HttpApi.Host/MediaInAction.FileService.HttpApi.Host.csproj" "services/file/src/MediaInAction.FileService.HttpApi.Host/MediaInAction.FileService.HttpApi.Host.csproj"
COPY "services/file/src/MediaInAction.FileService.Domain/MediaInAction.FileService.Domain.csproj" "services/file/src/MediaInAction.FileService.Domain/MediaInAction.FileService.Domain.csproj"
COPY "services/file/src/MediaInAction.FileService.BG.Application.Contracts/MediaInAction.FileService.BG.Application.Contracts.csproj" "services/file/src/MediaInAction.FileService.BG.Application.Contracts/MediaInAction.FileService.BG.Application.Contracts.csproj"
COPY "services/file/src/MediaInAction.FileService.Application/MediaInAction.FileService.Application.csproj" "services/file/src/MediaInAction.FileService.Application/MediaInAction.FileService.Application.csproj"
COPY "services/file/src/MediaInAction.FileService.BG.HttpApi.Host/MediaInAction.FileService.BG.HttpApi.Host.csproj" "services/file/src/MediaInAction.FileService.BG.HttpApi.Host/MediaInAction.FileService.BG.HttpApi.Host.csproj"
COPY "services/file/src/MediaInAction.FileService.Application.Contracts/MediaInAction.FileService.Application.Contracts.csproj" "services/file/src/MediaInAction.FileService.Application.Contracts/MediaInAction.FileService.Application.Contracts.csproj"
COPY "services/file/src/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj" "services/file/src/MediaInAction.FileService.HttpApi.Client/MediaInAction.FileService.HttpApi.Client.csproj"
COPY "services/file/src/MediaInAction.FileService.Lib/MediaInAction.FileService.Lib.csproj" "services/file/src/MediaInAction.FileService.Lib/MediaInAction.FileService.Lib.csproj"
COPY "services/file/src/MediaInAction.FileService.MongoDb/MediaInAction.FileService.MongoDb.csproj" "services/file/src/MediaInAction.FileService.MongoDb/MediaInAction.FileService.MongoDb.csproj"
COPY "services/file/src/MediaInAction.FileService.HttpApi/MediaInAction.FileService.HttpApi.csproj" "services/file/src/MediaInAction.FileService.HttpApi/MediaInAction.FileService.HttpApi.csproj"
COPY "services/file/src/MediaInAction.FileService.Domain.Shared/MediaInAction.FileService.Domain.Shared.csproj" "services/file/src/MediaInAction.FileService.Domain.Shared/MediaInAction.FileService.Domain.Shared.csproj"
COPY "services/file/src/MediaInAction.FileService.BG.Application/MediaInAction.FileService.BG.Application.csproj" "services/file/src/MediaInAction.FileService.BG.Application/MediaInAction.FileService.BG.Application.csproj"
COPY "services/file/test/test/test.csproj" "services/file/test/test/test.csproj"
COPY "services/file/test/MediaInAction.FileService.Domain.Tests/MediaInAction.FileService.Domain.Tests.csproj" "services/file/test/MediaInAction.FileService.Domain.Tests/MediaInAction.FileService.Domain.Tests.csproj"
COPY "services/file/test/MediaInAction.FileService.HttpApi.Client.ConsoleTestApp/MediaInAction.FileService.HttpApi.Client.ConsoleTestApp.csproj" "services/file/test/MediaInAction.FileService.HttpApi.Client.ConsoleTestApp/MediaInAction.FileService.HttpApi.Client.ConsoleTestApp.csproj"
COPY "services/file/test/MediaInAction.FileService.MongoDb.Tests/MediaInAction.FileService.MongoDb.Tests.csproj" "services/file/test/MediaInAction.FileService.MongoDb.Tests/MediaInAction.FileService.MongoDb.Tests.csproj"
COPY "services/file/test/MediaInAction.FileService.TestBase/MediaInAction.FileService.TestBase.csproj" "services/file/test/MediaInAction.FileService.TestBase/MediaInAction.FileService.TestBase.csproj"
COPY "services/file/test/MediaInAction.FileService.Application.Tests/MediaInAction.FileService.Application.Tests.csproj" "services/file/test/MediaInAction.FileService.Application.Tests/MediaInAction.FileService.Application.Tests.csproj"

# video
COPY "services/video/src/MediaInAction.VideoService.HttpApi/MediaInAction.VideoService.HttpApi.csproj" "services/video/src/MediaInAction.VideoService.HttpApi/MediaInAction.VideoService.HttpApi.csproj"
COPY "services/video/src/MediaInAction.VideoService.HttpApi.Host/MediaInAction.VideoService.HttpApi.Host.csproj" "services/video/src/MediaInAction.VideoService.HttpApi.Host/MediaInAction.VideoService.HttpApi.Host.csproj"
COPY "services/video/src/MediaInAction.VideoService.Application.Contracts/MediaInAction.VideoService.Application.Contracts.csproj" "services/video/src/MediaInAction.VideoService.Application.Contracts/MediaInAction.VideoService.Application.Contracts.csproj"
COPY "services/video/src/MediaInAction.VideoService.Domain/MediaInAction.VideoService.Domain.csproj" "services/video/src/MediaInAction.VideoService.Domain/MediaInAction.VideoService.Domain.csproj"
COPY "services/video/src/MediaInAction.VideoService.Domain.Shared/MediaInAction.VideoService.Domain.Shared.csproj" "services/video/src/MediaInAction.VideoService.Domain.Shared/MediaInAction.VideoService.Domain.Shared.csproj"
COPY "services/video/src/MediaInAction.VideoService.EntityFrameworkCore/MediaInAction.VideoService.EntityFrameworkCore.csproj" "services/video/src/MediaInAction.VideoService.EntityFrameworkCore/MediaInAction.VideoService.EntityFrameworkCore.csproj"
COPY "services/video/src/MediaInAction.VideoService.Lib/MediaInAction.VideoService.Lib.csproj" "services/video/src/MediaInAction.VideoService.Lib/MediaInAction.VideoService.Lib.csproj"
COPY "services/video/src/MediaInAction.VideoService.HttpApi.Client/MediaInAction.VideoService.HttpApi.Client.csproj" "services/video/src/MediaInAction.VideoService.HttpApi.Client/MediaInAction.VideoService.HttpApi.Client.csproj"
COPY "services/video/src/MediaInAction.VideoService.BG.Workers/MediaInAction.VideoService.BG.Workers.csproj" "services/video/src/MediaInAction.VideoService.BG.Workers/MediaInAction.VideoService.BG.Workers.csproj"
COPY "services/video/src/MediaInAction.VideoService.Application/MediaInAction.VideoService.Application.csproj" "services/video/src/MediaInAction.VideoService.Application/MediaInAction.VideoService.Application.csproj"
COPY "services/video/test/test/test.csproj" "services/video/test/test/test.csproj"
COPY "services/video/test/MediaInAction.VideoService.HttpApi.Client.ConsoleTestApp/MediaInAction.VideoService.HttpApi.Client.ConsoleTestApp.csproj" "services/video/test/MediaInAction.VideoService.HttpApi.Client.ConsoleTestApp/MediaInAction.VideoService.HttpApi.Client.ConsoleTestApp.csproj"
COPY "services/video/test/MediaInAction.VideoService.Domain.Tests/MediaInAction.VideoService.Domain.Tests.csproj" "services/video/test/MediaInAction.VideoService.Domain.Tests/MediaInAction.VideoService.Domain.Tests.csproj"
COPY "services/video/test/MediaInAction.VideoService.EntityFrameworkCore.Tests/MediaInAction.VideoService.EntityFrameworkCore.Tests.csproj" "services/video/test/MediaInAction.VideoService.EntityFrameworkCore.Tests/MediaInAction.VideoService.EntityFrameworkCore.Tests.csproj"
COPY "services/video/test/MediaInAction.VideoService.TestBase/MediaInAction.VideoService.TestBase.csproj" "services/video/test/MediaInAction.VideoService.TestBase/MediaInAction.VideoService.TestBase.csproj"
COPY "services/video/test/MediaInAction.VideoService.Application.Tests/MediaInAction.VideoService.Application.Tests.csproj" "services/video/test/MediaInAction.VideoService.Application.Tests/MediaInAction.VideoService.Application.Tests.csproj"

# trakt
COPY "services/trakt/src/MediaInAction.TraktService.Domain.Shared/MediaInAction.TraktService.Domain.Shared.csproj" "services/trakt/src/MediaInAction.TraktService.Domain.Shared/MediaInAction.TraktService.Domain.Shared.csproj"
COPY "services/trakt/src/MediaInAction.TraktService.Application/MediaInAction.TraktService.Application.csproj" "services/trakt/src/MediaInAction.TraktService.Application/MediaInAction.TraktService.Application.csproj"
COPY "services/trakt/src/MediaInAction.TraktService.HttpApi/MediaInAction.TraktService.HttpApi.csproj" "services/trakt/src/MediaInAction.TraktService.HttpApi/MediaInAction.TraktService.HttpApi.csproj"
COPY "services/trakt/src/MediaInAction.TraktService.MongoDb/MediaInAction.TraktService.MongoDb.csproj" "services/trakt/src/MediaInAction.TraktService.MongoDb/MediaInAction.TraktService.MongoDb.csproj"
COPY "services/trakt/src/MediaInAction.TraktService.Lib/MediaInAction.TraktService.Lib.csproj" "services/trakt/src/MediaInAction.TraktService.Lib/MediaInAction.TraktService.Lib.csproj"
COPY "services/trakt/src/MediaInAction.TraktService.BG.Workers/MediaInAction.TraktService.BG.Workers.csproj" "services/trakt/src/MediaInAction.TraktService.BG.Workers/MediaInAction.TraktService.BG.Workers.csproj"
COPY "services/trakt/src/MediaInAction.TraktService.BG.Jobs/MediaInAction.TraktService.BG.Jobs.csproj" "services/trakt/src/MediaInAction.TraktService.BG.Jobs/MediaInAction.TraktService.BG.Jobs.csproj"
COPY "services/trakt/src/MediaInAction.TraktService.HttpApi.Client/MediaInAction.TraktService.HttpApi.Client.csproj" "services/trakt/src/MediaInAction.TraktService.HttpApi.Client/MediaInAction.TraktService.HttpApi.Client.csproj"
COPY "services/trakt/src/MediaInAction.TraktService.HttpApi.Host/MediaInAction.TraktService.HttpApi.Host.csproj" "services/trakt/src/MediaInAction.TraktService.HttpApi.Host/MediaInAction.TraktService.HttpApi.Host.csproj"
COPY "services/trakt/src/MediaInAction.TraktService.Domain/MediaInAction.TraktService.Domain.csproj" "services/trakt/src/MediaInAction.TraktService.Domain/MediaInAction.TraktService.Domain.csproj"
COPY "services/trakt/src/MediaInAction.TraktService.Application.Contracts/MediaInAction.TraktService.Application.Contracts.csproj" "services/trakt/src/MediaInAction.TraktService.Application.Contracts/MediaInAction.TraktService.Application.Contracts.csproj"
COPY "services/trakt/test/MediaInAction.TraktService.HttpApi.Client.ConsoleTestApp/MediaInAction.TraktService.HttpApi.Client.ConsoleTestApp.csproj" "services/trakt/test/MediaInAction.TraktService.HttpApi.Client.ConsoleTestApp/MediaInAction.TraktService.HttpApi.Client.ConsoleTestApp.csproj"
COPY "services/trakt/test/test/test.csproj" "services/trakt/test/test/test.csproj"
COPY "services/trakt/test/MediaInAction.TraktService.Application.Tests/MediaInAction.TraktService.Application.Tests.csproj" "services/trakt/test/MediaInAction.TraktService.Application.Tests/MediaInAction.TraktService.Application.Tests.csproj"
COPY "services/trakt/test/MediaInAction.TraktService.EntityFrameworkCore.Tests/MediaInAction.TraktService.EntityFrameworkCore.Tests.csproj" "services/trakt/test/MediaInAction.TraktService.EntityFrameworkCore.Tests/MediaInAction.TraktService.EntityFrameworkCore.Tests.csproj"
COPY "services/trakt/test/MediaInAction.TraktService.MongoDb.Tests/MediaInAction.TraktService.MongoDb.Tests.csproj" "services/trakt/test/MediaInAction.TraktService.MongoDb.Tests/MediaInAction.TraktService.MongoDb.Tests.csproj"
COPY "services/trakt/test/MediaInAction.TraktService.TestBase/MediaInAction.TraktService.TestBase.csproj" "services/trakt/test/MediaInAction.TraktService.TestBase/MediaInAction.TraktService.TestBase.csproj"
COPY "services/trakt/test/MediaInAction.TraktService.Domain.Tests/MediaInAction.TraktService.Domain.Tests.csproj" "services/trakt/test/MediaInAction.TraktService.Domain.Tests/MediaInAction.TraktService.Domain.Tests.csproj"

# shared
COPY "shared/MediaInAction.Shared.Integration/MediaInAction.Shared.Integration.csproj" "shared/MediaInAction.Shared.Integration/MediaInAction.Shared.Integration.csproj"
COPY "shared/MediaInAction.Shared.HealthChecks/MediaInAction.Shared.HealthChecks.csproj" "shared/MediaInAction.Shared.HealthChecks/MediaInAction.Shared.HealthChecks.csproj"
COPY "shared/MediaInAction.Keycloak.DbMigrator/MediaInAction.Keycloak.DbMigrator.csproj" "shared/MediaInAction.Keycloak.DbMigrator/MediaInAction.Keycloak.DbMigrator.csproj"
COPY "shared/MediaInAction.Shared.Domain/MediaInAction.Shared.Domain.csproj" "shared/MediaInAction.Shared.Domain/MediaInAction.Shared.Domain.csproj"
COPY "shared/MediaInAction.Shared.Grpc/MediaInAction.Shared.Grpc.csproj" "shared/MediaInAction.Shared.Grpc/MediaInAction.Shared.Grpc.csproj"
COPY "shared/MediaInAction.Shared.Hosting.Microservices/MediaInAction.Shared.Hosting.Microservices.csproj" "shared/MediaInAction.Shared.Hosting.Microservices/MediaInAction.Shared.Hosting.Microservices.csproj"
COPY "shared/MediaInAction.Shared.Hosting.AspNetCore/MediaInAction.Shared.Hosting.AspNetCore.csproj" "shared/MediaInAction.Shared.Hosting.AspNetCore/MediaInAction.Shared.Hosting.AspNetCore.csproj"
COPY "shared/MediaInAction.Shared.Hosting/MediaInAction.Shared.Hosting.csproj" "shared/MediaInAction.Shared.Hosting/MediaInAction.Shared.Hosting.csproj"
COPY "shared/MediaInAction.Shared.Helpers/MediaInAction.Shared.Helpers.csproj" "shared/MediaInAction.Shared.Helpers/MediaInAction.Shared.Helpers.csproj"
COPY "shared/MediaInAction.Shared.Hosting.Gateways/MediaInAction.Shared.Hosting.Gateways.csproj" "shared/MediaInAction.Shared.Hosting.Gateways/MediaInAction.Shared.Hosting.Gateways.csproj"
COPY "shared/MediaInAction.Shared.Api/MediaInAction.Shared.Api.csproj" "shared/MediaInAction.Shared.Api/MediaInAction.Shared.Api.csproj"
COPY "shared/MediaInAction.Shared.Localization/MediaInAction.Shared.Localization.csproj" "shared/MediaInAction.Shared.Localization/MediaInAction.Shared.Localization.csproj"

# libraries
COPY "libs/EmbyClient.Dotnet/EmbyClient.Dotnet.csproj" "libs/EmbyClient.Dotnet/EmbyClient.Dotnet.csproj"
COPY "libs/DelugeRPCClient.Net/DelugeRPCClient.Net.csproj" "libs/DelugeRPCClient.Net/DelugeRPCClient.Net.csproj"

# End build cache

WORKDIR /work
RUN dotnet restore "MediaInAction.sln"
COPY . .
WORKDIR /work/src/services/cmskit/MediaInAction.CmskitService.HttpApi.Host
RUN dotnet publish  -c Release -o /app

FROM build as app_tests
WORKDIR /work/src/services/cmskit/MediaInAction.CmskitService.Application.Tests

FROM build as domain_tests
WORKDIR /work/src/services/cmskit/MediaInAction.CmskitService.Domain.Tests

FROM build AS publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MediaInAction.CmskitService.HttpApi.Host.dll"]
