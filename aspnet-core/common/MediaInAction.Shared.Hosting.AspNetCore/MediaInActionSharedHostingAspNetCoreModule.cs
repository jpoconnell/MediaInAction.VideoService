using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Modularity;
using Volo.Abp.Swashbuckle;

namespace MediaInAction.Shared.Hosting.AspNetCore;

[DependsOn(
    typeof(MediaInActionSharedHostingModule),
    typeof(MediaInActionSharedLocalizationModule),
    typeof(AbpAspNetCoreSerilogModule),
    typeof(AbpSwashbuckleModule)
)]
public class MediaInActionSharedHostingAspNetCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
    }
}