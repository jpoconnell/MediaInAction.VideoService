using MediaInAction.TraktService.MongoDB;
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
using MediaInAction.TraktService.Bg;
using MediaInAction.TraktService.DbMigrations;
using Volo.Abp;
using Volo.Abp.Application;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;
using Volo.Abp.AutoMapper;
using Volo.Abp.Caching;
using Volo.Abp.Domain;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;

namespace MediaInAction.TraktService;

[DependsOn(
    typeof(AbpAspNetCoreMvcModule),
    typeof(AbpHttpClientModule),
    typeof(AbpAutoMapperModule),
    typeof(AbpCachingModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpDddDomainModule),
    typeof(TraktServiceHttpApiModule),
    typeof(TraktServiceApplicationModule),
    typeof(TraktServiceMongoDbModule),
    typeof(TraktServiceBgWorkerModule),
    typeof(MediaInActionSharedHostingMicroservicesModule)
)]
public class TraktServiceHttpApiHostModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        JwtBearerConfigurationHelper.Configure(context, "TraktService");

        SwaggerConfigurationHelper.ConfigureWithOidc(
            context: context,
            authority: configuration["AuthServer:Authority"]!,
            scopes: ["TraktService"],
            discoveryEndpoint: configuration["AuthServer:MetadataAddress"],
            apiTitle: "Trakt Service API" 
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
            options.ConventionalControllers.Create(typeof(TraktServiceApplicationModule).Assembly, opts =>
            {
                opts.RootPath = "trakt";
                opts.RemoteServiceName = "Trakt";
            });
        });

        Configure<AbpUnitOfWorkDefaultOptions>(options =>
        {
            //Standalone MongoDB servers don't support transactions
            options.TransactionBehavior = UnitOfWorkTransactionBehavior.Disabled;
        });

        Configure<AbpAntiForgeryOptions>(options => { options.AutoValidate = false; });
        context.Services.AddGrpc(options =>
        {
            options.EnableDetailedErrors = true;
        });
    }
    
    private void ConfigureGrpc(ServiceConfigurationContext context)
    {
        // context.Services.AddHttpForwarderWithServiceDiscovery();
        
        context.Services.AddGrpcClient<Seriesgrpc.SeriesGrpcService.SeriesGrpcServiceClient>((services, options) =>
        {
            options.Address = new Uri("http://_grpc.videoService");
        });
        
        context.Services.AddGrpcClient<Moviegrpc.MovieGrpcService.MovieGrpcServiceClient>((services, options) =>
        {
            options.Address = new Uri("http://_grpc.videoService");
        });

        context.Services.AddGrpcClient<Episodegrpc.EpisodeGrpcService.EpisodeGrpcServiceClient>((services, options) =>
        {
            options.Address = new Uri("http://_grpc.videoService");
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
        app.MapAbpStaticAssets();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAbpClaimsMap();
        app.UseAuthorization();
        app.UseSwagger();
        app.UseAbpSwaggerWithCustomScriptUI(options =>
        {
            var configuration = context.ServiceProvider.GetRequiredService<IConfiguration>();
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Trakt Service API");
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
            .GetRequiredService<TraktServiceDatabaseMigrationChecker>()
            .CheckAndApplyDatabaseMigrationsAsync();
    }
}