using MediaInAction.VideoService.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace MediaInAction.VideoService
{
    [DependsOn(
        typeof(VideoServiceDomainModule),
        typeof(VideoServiceEntityFrameworkCoreModule) 
    )]
    public class VideoServiceLibModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {  
            var configuration = context.Services.GetConfiguration();
            var hostingEnvironment = context.Services.GetHostingEnvironment();
            
            Configure<AbpAutoMapperOptions>(options => { options.AddMaps<VideoServiceLibModule>(); });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            
            // ... others
        }
    }
}
