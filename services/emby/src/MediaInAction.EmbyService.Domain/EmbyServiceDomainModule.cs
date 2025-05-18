using Volo.Abp.AutoMapper;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace MediaInAction.EmbyService;

[DependsOn(
    typeof(EmbyServiceDomainSharedModule),
    typeof(AbpDddDomainModule),
    typeof(AbpAutoMapperModule)
)]
public class EmbyServiceDomainModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
    }
}