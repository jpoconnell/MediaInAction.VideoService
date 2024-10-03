﻿using MediaInAction.VideoService.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.PostgreSql;

namespace MediaInAction.VideoService.Integration.Test.Fixtures
{
    public class DockerWebApplicationFactoryFixture : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private  PostgreSqlContainer   _dbContainer;
        public int InitialSeriesCount { get; } = 3;


        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var connectionString = _dbContainer.GetConnectionString();
            base.ConfigureWebHost(builder);
            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<VideoServiceDbContext>));
                services.AddDbContext<VideoServiceDbContext>(options =>
                {
                    options.UseNpgsql(connectionString);
                });
            });
        }

        public async Task InitializeAsync()
        {
            await _dbContainer.StartAsync();
        }

        public async Task DisposeAsync()
        {
            await _dbContainer.StopAsync();
        }
    }
}
