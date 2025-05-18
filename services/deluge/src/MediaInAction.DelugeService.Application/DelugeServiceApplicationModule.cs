using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace MediaInAction.DelugeService;

[DependsOn(
    typeof(DelugeServiceDomainModule),
    typeof(DelugeServiceApplicationContractsModule),
    typeof(AbpDddApplicationModule)
)]
public class DelugeServiceApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options => { options.AddMaps<DelugeServiceApplicationModule>(); });
    }
}