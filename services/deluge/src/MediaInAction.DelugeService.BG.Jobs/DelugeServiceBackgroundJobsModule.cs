using MediaInAction.DelugeService.Bg.DelugeTorrentNs;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp;
using Volo.Abp.BackgroundJobs.Quartz;
using Volo.Abp.Modularity;

namespace MediaInAction.DelugeService.Bg
{

    [DependsOn(
        typeof(AbpBackgroundJobsQuartzModule),
        typeof(DelugeServiceDomainModule)
    )]
    
    public class DelugeServiceBackgroundJobsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.TryAddSingleton<IDelugeTorrentService, DelugeTorrentService>();
        }
        
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var configuration = context.GetConfiguration();
            
          }
    }
}
