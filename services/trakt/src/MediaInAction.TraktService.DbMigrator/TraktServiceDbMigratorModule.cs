using MediaInAction.TraktService.MongoDB;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace MediaInAction.TraktService.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(TraktServiceMongoDbModule),
    typeof(TraktServiceApplicationContractsModule)
    )]
public class TraktServiceDbMigratorModule : AbpModule
{

}
