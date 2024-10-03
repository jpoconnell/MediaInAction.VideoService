using System.Threading.Tasks;
using MediaInAction.DelugeService.Bg.DelugeTorrentNs;
using MediaInAction.DelugeService.Bg.Workers;
using MediaInAction.DelugeService.MongoDB;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.BackgroundWorkers.Quartz;
using Volo.Abp.Modularity;
using Volo.Abp.Quartz;

namespace MediaInAction.DelugeService.Bg;

[DependsOn(
    typeof(AbpBackgroundWorkersQuartzModule),
    typeof(DelugeServiceLibModule),
    typeof(DelugeServiceDomainSharedModule),
    typeof(DelugeServiceMongoDbModule) 
)]
public class DelugeServiceBgWorkerModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        PreConfigure<AbpQuartzOptions>(options =>
        {
            options.Configurator = configure =>
            {
                configure.UseInMemoryStore(storeOptions =>
                {
                });
            };
        });
    }
    
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.TryAddSingleton<IDelugeTorrentService, DelugeTorrentService>();
        context.Services.TryAddSingleton<IDelugeService, DelugeService>();
        context.Services.TryAddSingleton<DelugeServicesConfiguration, DelugeServicesConfiguration>();

        Configure<AbpBackgroundWorkerQuartzOptions>(options =>
        {
            options.IsAutoRegisterEnabled = true;
        });
    }
    
    public override async Task OnApplicationInitializationAsync(
        ApplicationInitializationContext context)
    {
        await context.AddBackgroundWorkerAsync<GetTorrentsWorker>();
    }
}
