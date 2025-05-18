using MediaInAction.EmbyService.DbMigrations;
using MediaInAction.EmbyService.MongoDB;
using MediaInAction.Shared.Hosting.AspNetCore;
using MediaInAction.Shared.Hosting.Microservices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading.Tasks;
using MediaInAction.EmbyService.Bg;
using VideoService.Episode.GrpcServer;
using VideoService.Movie.GrpcServer;
using VideoService.Series.GrpcServer;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;

namespace MediaInAction.EmbyService;

[DependsOn(
    typeof(EmbyServiceHttpApiModule),
    typeof(EmbyServiceApplicationModule),
    typeof(EmbyServiceMongoDbModule),
    //typeof(EmbyServiceBgWorkerModule),
    typeof(MediaInActionSharedHostingMicroservicesModule)
)]
public class EmbyServiceHttpApiHostModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        JwtBearerConfigurationHelper.Configure(context, "EmbyService");

        SwaggerConfigurationHelper.ConfigureWithOidc(
            context: context,
            authority: configuration["AuthServer:Authority"]!,
            scopes: ["EmbyService"],
            discoveryEndpoint: configuration["AuthServer:MetadataAddress"],
            apiTitle: "Emby Service API" 
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

        ConfigureGrpc(context);
        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers.Create(typeof(EmbyServiceApplicationModule).Assembly, opts =>
            {
                opts.RootPath = "emby";
                opts.RemoteServiceName = "EmbyService";
            });
        });

        Configure<AbpUnitOfWorkDefaultOptions>(options =>
        {
            //Standalone MongoDB servers don't support transactions
            options.TransactionBehavior = UnitOfWorkTransactionBehavior.Disabled;
        });

        Configure<AbpAntiForgeryOptions>(options => { options.AutoValidate = false; });
    }

    private void ConfigureGrpc(ServiceConfigurationContext context)
    {
        // context.Services.AddHttpForwarderWithServiceDiscovery();
         
        context.Services.AddGrpcClient<SeriesGrpcService.SeriesGrpcServiceClient>((services, options) =>
        {
            options.Address = new Uri("https://localhost:44356");
        });
        context.Services.AddGrpcClient<MovieGrpcService.MovieGrpcServiceClient>((services, options) =>
        {
            options.Address = new Uri("https://localhost:44356");
        });

        context.Services.AddGrpcClient<EpisodeGrpcService.EpisodeGrpcServiceClient>((services, options) =>
        {
            options.Address = new Uri("https://localhost:44356");
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
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
      //  app.UseAbpClaimsMap();
        app.UseAuthorization();
        app.UseSwagger();
        app.UseAbpSwaggerWithCustomScriptUI(options =>
        {
            var configuration = context.ServiceProvider.GetRequiredService<IConfiguration>();
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Emby Service API");
            options.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
        });
        app.UseAbpSerilogEnrichers();
        app.UseAuditing();
        app.UseUnitOfWork();
        app.UseConfiguredEndpoints();
    }

    public override async Task OnPostApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        await context.ServiceProvider
            .GetRequiredService<EmbyServiceDatabaseMigrationChecker>()
            .CheckAndApplyDatabaseMigrationsAsync();
    }
}