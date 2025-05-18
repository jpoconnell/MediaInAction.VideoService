using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;

namespace MediaInAction.EmbyService;

[DependsOn(
    typeof(EmbyServiceDomainSharedModule),
    typeof(AbpObjectExtendingModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
)]
public class EmbyServiceApplicationContractsModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        EmbyServiceDtoExtensions.Configure();
    }
}