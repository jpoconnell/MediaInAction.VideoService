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
using MediaInAction.FileService.BG.Workers;
using MediaInAction.FileService.DbMigrations;
using MediaInAction.FileService.MongoDb;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;

namespace MediaInAction.FileService;

[DependsOn(
    typeof(FileServiceHttpApiModule),
    typeof(FileServiceApplicationModule),
    typeof(FileServiceMongoDbModule),
    typeof(FileServiceBgWorkerModule),
    typeof(MediaInActionSharedHostingMicroservicesModule)
)]
public class FileServiceHttpApiHostModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        JwtBearerConfigurationHelper.Configure(context, "FileService");

        SwaggerConfigurationHelper.ConfigureWithOidc(
            context: context,
            authority: configuration["AuthServer:Authority"]!,
            scopes: ["FileService"],
            discoveryEndpoint: configuration["AuthServer:MetadataAddress"],
            apiTitle: "File Service API" 
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

        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers.Create(typeof(FileServiceApplicationModule).Assembly, opts =>
            {
                opts.RootPath = "file";
                opts.RemoteServiceName = "File";
            });
        });

        Configure<AbpUnitOfWorkDefaultOptions>(options =>
        {
            //Standalone MongoDB servers don't support transactions
            options.TransactionBehavior = UnitOfWorkTransactionBehavior.Disabled;
        });

        Configure<AbpAntiForgeryOptions>(options => { options.AutoValidate = false; });
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
        app.UseAbpClaimsMap();
        app.UseAuthorization();
        app.UseSwagger();
        app.UseAbpSwaggerWithCustomScriptUI(options =>
        {
            var configuration = context.ServiceProvider.GetRequiredService<IConfiguration>();
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "File Service API");
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
            .GetRequiredService<FileServiceDatabaseMigrationChecker>()
            .CheckAndApplyDatabaseMigrationsAsync();
    }
}