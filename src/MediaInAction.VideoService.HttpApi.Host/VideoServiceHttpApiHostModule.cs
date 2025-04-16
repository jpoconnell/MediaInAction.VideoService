using MediaInAction.VideoService.EntityFrameworkCore;
using MediaInAction.Shared.Hosting.AspNetCore;
using MediaInAction.Shared.Hosting.Microservices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using VideoService.Episode.GrpcServer;
using VideoService.Movie.GrpcServer;
using VideoService.Series.GrpcServer;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace MediaInAction.VideoService;

[DependsOn(
    typeof(VideoServiceHttpApiModule),
    typeof(VideoServiceApplicationModule),
    typeof(VideoServiceEntityFrameworkCoreModule),
    typeof(MediaInActionSharedHostingMicroservicesModule)
)]
public class VideoServiceHttpApiHostModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        var hostingEnvironment = context.Services.GetHostingEnvironment();

        JwtBearerConfigurationHelper.Configure(context, "VideoService");

        SwaggerConfigurationHelper.ConfigureWithOidc(
            context: context,
            authority: configuration["AuthServer:Authority"]!,
            scopes: ["VideoService"],
            discoveryEndpoint: configuration["AuthServer:MetadataAddress"],
            apiTitle: "Video Service API"
            );

        context.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder
                    .WithOrigins(
                        configuration["App:CorsOrigins"]!
                            .Split(",", StringSplitOptions.RemoveEmptyEntries)
                            .Select(o => o.Trim().RemovePostFix("/"))
                            .ToArray()
                    )
                    .WithAbpExposedHeaders()
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        // TODO: Crate controller instead of auto-controller configuration
        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers.Create(typeof(VideoServiceApplicationModule).Assembly, opts =>
            {
                opts.RootPath = "video";
                opts.RemoteServiceName = "Video";
            });
        });
        
        context.Services.AddGrpc(options =>
        {
            options.EnableDetailedErrors = true;
        });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseCorrelationId();
        app.UseCors();
        app.UseAbpRequestLocalization();
       // app.MapAbpStaticAssets();
        app.UseRouting();
        app.UseAuthentication();
        //app.UseAbpClaimsMap();
        app.UseAuthorization();
        app.UseSwagger();
        app.UseAbpSwaggerWithCustomScriptUI(options =>
        {
            var configuration = context.ServiceProvider.GetRequiredService<IConfiguration>();
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Video Service API");
            options.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
        });
        app.UseAbpSerilogEnrichers();
        app.UseAuditing();
        app.UseUnitOfWork();
        app.UseConfiguredEndpoints(endpoints =>
        {
            endpoints.MapGrpcService<SeriesGrpcService.SeriesGrpcServiceClient>();
            endpoints.MapGrpcService<EpisodeGrpcService.EpisodeGrpcServiceBase>();
            endpoints.MapGrpcService<MovieGrpcService.MovieGrpcServiceBase>();
            endpoints.MapGrpcService<MovieGrpcService.MovieGrpcServiceBase>();
        });
    }
}