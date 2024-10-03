using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace MediaInAction.FileService.BG;

[DependsOn(
    typeof(FileServiceDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
)]
public class FileServiceBGApplicationContractsModule : AbpModule
{

}