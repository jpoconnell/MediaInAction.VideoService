using MediaInAction.TraktService.TraktShowNs;
using Volo.Abp.Domain;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.Modularity;

namespace MediaInAction.TraktService;

[DependsOn(
    typeof(TraktServiceDomainSharedModule),
    typeof(AbpDddDomainModule)
)]
public class TraktServiceDomainModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        /*
        Configure<AbpDistributedEntityEventOptions>(options =>
        {
            options.AutoEventSelectors.Add<TraktShow>();
            options.EtoMappings.Add<TraktShow, TraktShowCreatedEto>();
        });
        */
    }
}