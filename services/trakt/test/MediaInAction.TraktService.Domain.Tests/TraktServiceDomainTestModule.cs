using MediaInAction.TraktService.MongoDB;
using Volo.Abp.Modularity;

namespace MediaInAction.TraktService
{
    [DependsOn(
        typeof(TraktServiceMongoDbTestModule)
        )]
    public class TraktServiceDomainTestModule : AbpModule
    {

    }
}