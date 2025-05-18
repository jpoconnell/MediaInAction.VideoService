using MediaInAction.FileService.FileEntriesNs;
using Volo.Abp.AutoMapper;
using Volo.Abp.Domain;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.Modularity;

namespace MediaInAction.FileService;

[DependsOn(
    typeof(FileServiceDomainSharedModule),
    typeof(AbpDddDomainModule),
    typeof(AbpAutoMapperModule)
)]
public class FileServiceDomainModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {

    }
}