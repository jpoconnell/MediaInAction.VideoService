using MediaInAction.EmbyService.MongoDB;
using Volo.Abp.Modularity;

namespace MediaInAction.EmbyService
{
    [DependsOn(
        typeof(EmbyServiceMongoDbTestModule)
        )]
    public class EmbyServiceDomainTestModule : AbpModule
    {

    }
}