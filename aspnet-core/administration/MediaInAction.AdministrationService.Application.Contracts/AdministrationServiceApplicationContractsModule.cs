using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using MediaInAction.TraktService;
using MediaInAction.VideoService;
using MediaInAction.CmskitService;

namespace MediaInAction.AdministrationService
{
    [DependsOn(
        typeof(TraktServiceApplicationContractsModule),
        typeof(AdministrationServiceDomainSharedModule),
        typeof(AbpPermissionManagementApplicationContractsModule),
        typeof(AbpSettingManagementApplicationContractsModule),
        typeof(VideoServiceApplicationContractsModule),
        typeof(CmskitServiceApplicationContractsModule)
    )]
    public class AdministrationServiceApplicationContractsModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
        }
    }
}
