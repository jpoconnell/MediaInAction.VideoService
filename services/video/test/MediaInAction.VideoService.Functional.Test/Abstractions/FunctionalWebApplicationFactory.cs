using MediaInAction.AdministrationService.EntityFrameworkCore;
using MediaInAction.VideoService.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.PostgreSql;

namespace MediaInAction.VideoService.Functional.Test.Abstractions
{
    public class FunctionalWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private  PostgreSqlContainer _dbContainer;

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
                services.RemoveAll(typeof(DbContextOptions<AdministrationServiceDbContext>));
                services.AddDbContext<AdministrationServiceDbContext>(options =>
                {
                    options.UseNpgsql(connectionString);
                });
            });
        }

        public async Task InitializeAsync()
        {
            await _dbContainer.StartAsync();
        }

        public new async Task DisposeAsync()
        {
            await _dbContainer.StopAsync();
        }
    }
}
