using FluentAssertions.Common;
using MediaInAction.EmbyService;
using MediaInAction.VideoService.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MediaInAction.VideoService.Integration.Tests;

internal class VideoServiceWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<VideoServiceDbContext>));
        });
    }

    private static string GetConnectionString()
    {
        var configuration = new ConfigurationBuilder()
            .AddUserSecrets<VideoServiceWebApplicationFactory>()
            .Build();
        
        var connstring = configuration.GetConnectionString("VideoService");
        return connstring;
       
    }
}