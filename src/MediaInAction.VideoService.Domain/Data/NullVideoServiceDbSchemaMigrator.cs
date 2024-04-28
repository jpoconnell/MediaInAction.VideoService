using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.VideoService.Data;

/* This is used if database provider does't define
 * IVideoServiceDbSchemaMigrator implementation.
 */
public class NullVideoServiceDbSchemaMigrator : IVideoServiceDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
