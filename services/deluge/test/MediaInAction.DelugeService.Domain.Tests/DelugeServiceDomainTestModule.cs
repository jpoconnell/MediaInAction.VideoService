using MediaInAction.DelugeService.MongoDB;
using Volo.Abp.Modularity;

namespace MediaInAction.DelugeService
{
    [DependsOn(
        typeof(DelugeServiceMongoDbTestModule)
        )]
    public class DelugeServiceDomainTestModule : AbpModule
    {

    }
}