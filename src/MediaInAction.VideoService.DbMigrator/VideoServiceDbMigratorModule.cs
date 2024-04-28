using MediaInAction.VideoService.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace MediaInAction.VideoService.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(VideoServiceEntityFrameworkCoreModule),
    typeof(VideoServiceApplicationContractsModule)
    )]
public class VideoServiceDbMigratorModule : AbpModule
{
}
