using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace MediaInAction.EmbyService;

[DependsOn(
    typeof(EmbyServiceDomainModule),
    typeof(EmbyServiceApplicationContractsModule),
    typeof(AbpDddApplicationModule)
)]
public class EmbyServiceApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options => { options.AddMaps<EmbyServiceApplicationModule>(); });
    }
}