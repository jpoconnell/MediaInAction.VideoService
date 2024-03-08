using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MediaInAction.VideoService.Data;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.VideoService.EntityFrameworkCore;

public class EntityFrameworkCoreVideoServiceDbSchemaMigrator
    : IVideoServiceDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreVideoServiceDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the VideoServiceDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<VideoServiceDbContext>()
            .Database
            .MigrateAsync();
    }
}
