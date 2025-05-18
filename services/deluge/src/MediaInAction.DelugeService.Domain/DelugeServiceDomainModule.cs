using MediaInAction.DelugeService.DelugeTorrentsNs;
using Volo.Abp.AutoMapper;
using Volo.Abp.Domain;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.Modularity;

namespace MediaInAction.DelugeService;

[DependsOn(
    typeof(DelugeServiceDomainSharedModule),
    typeof(AbpDddDomainModule),
    typeof(AbpAutoMapperModule)
)]
public class DelugeServiceDomainModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options => { options.AddMaps<DelugeServiceDomainModule>(validate: true); });

        Configure<AbpDistributedEntityEventOptions>(options =>
        {
            options.AutoEventSelectors.Add<DelugeTorrent>();
            //options.EtoMappings.Add<DelugeTorrent, DelugeTorrentCreatedEto>();
        });
    }
}