﻿using MediaInAction.Shared.Hosting.AspNetCore;
using MediaInAction.Shared.Hosting.Gateways;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace MediaInAction.WebGateway;

[DependsOn(
    typeof(MediaInActionSharedHostingGatewaysModule)
)]
public class MediaInActionWebGatewayModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        var hostingEnvironment = context.Services.GetHostingEnvironment();

        SwaggerConfigurationHelper.ConfigureWithOidc(
            context: context,
            authority: configuration["AuthServer:Authority"]!,
            scopes:
            [
                /* Requested scopes for authorization code request and descriptions for swagger UI only */
                "IdentityService", "AdministrationService", "FileService", "EmbyService", "TraktService",
                "VideoService", "DelugeService", "CmskitService"
            ],
            apiTitle: "Web Gateway API",
            discoveryEndpoint: configuration["AuthServer:MetadataAddress"]
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
        
        context.Services.AddMemoryCache();
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
        app.UseAuthorization();
        app.UseSwaggerUIWithYarp(context);
        app.UseAbpSerilogEnrichers();

        app.UseRewriter(new RewriteOptions()
            // Regex for "", "/" and "" (whitespace)
            .AddRedirect("^(|\\|\\s+)$", "/swagger"));
        
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapReverseProxyWithLocalization();
        });
    }
}