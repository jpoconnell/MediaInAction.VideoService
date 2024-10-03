using MediaInAction.TraktService.Lib.Config;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace MediaInAction.TraktService.Lib
{
    [DependsOn(
        typeof(TraktServiceDomainModule)
    )]
    public class TraktServiceLibModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {  
            var configuration = context.Services.GetConfiguration();
            var hostingEnvironment = context.Services.GetHostingEnvironment();
            Configure<AbpAutoMapperOptions>(options => { options.AddMaps<TraktServiceLibModule>(); });
            Configure<ServicesConfiguration>(configuration.GetSection("Trakt"));
          
        }
        
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            
            // ... others
        }
        
    }
}