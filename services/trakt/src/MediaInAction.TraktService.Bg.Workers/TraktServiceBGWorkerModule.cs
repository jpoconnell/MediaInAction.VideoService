using System.Threading.Tasks;
using MediaInAction.TraktService.BG.Workers;
using MediaInAction.TraktService.Lib;
using MediaInAction.TraktService.Lib.Config;
using MediaInAction.TraktService.Lib.TraktEpisodeNs;
using MediaInAction.TraktService.Lib.TraktMovieNs;
using MediaInAction.TraktService.Lib.TraktShowNs;
using MediaInAction.TraktService.MongoDB;
using MediaInAction.TraktService.TraktEpisodeNs;
using MediaInAction.TraktService.TraktMovieNs;
using MediaInAction.TraktService.TraktShowNs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TraktNet;
using Volo.Abp;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.BackgroundWorkers.Quartz;
using Volo.Abp.Modularity;
using Volo.Abp.Quartz;

namespace MediaInAction.TraktService.Bg;

[DependsOn(
    typeof(AbpBackgroundWorkersQuartzModule),
    typeof(TraktServiceDomainSharedModule),
    typeof(TraktServiceMongoDbModule),
    typeof(TraktServiceLibModule)
)]
public class TraktServiceBgWorkerModule : AbpModule
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
        context.Services.TryAddSingleton<TraktEpisodeManager, TraktEpisodeManager>();
        context.Services.TryAddSingleton<ITraktEpisodeRepository, MongoDbTraktEpisodeRepository>();
        context.Services.TryAddSingleton<TraktMovieManager, TraktMovieManager>();
        context.Services.TryAddSingleton<ITraktMovieRepository, MongoDbTraktMovieRepository>();
        context.Services.TryAddSingleton<TraktShowManager, TraktShowManager>();
        context.Services.TryAddSingleton<ITraktShowRepository, MongoDbTraktShowRepository>();
        context.Services.TryAddSingleton<TraktClient, TraktClient>();
        
        /*
        context.Services.TryAddSingleton<EpisodeGrpcService.EpisodeGrpcServiceClient,EpisodeGrpcService.EpisodeGrpcServiceClient>();
        context.Services.TryAddSingleton<SeriesGrpcService.SeriesGrpcServiceClient, SeriesGrpcService.SeriesGrpcServiceClient>();
        */
        context.Services.TryAddSingleton<ServicesConfiguration, ServicesConfiguration>();
        context.Services.TryAddSingleton<ITraktService, Lib.TraktService>();
        context.Services.TryAddSingleton<ITraktShowLibService, TraktShowLibService>();
        context.Services.TryAddSingleton<ITraktEpisodeLibService, TraktEpisodeLibService>();
        context.Services.TryAddSingleton<ITraktMovieLibService, TraktMovieLibService>();
        context.Services.TryAddSingleton<ITraktShowPublicService, TraktShowPublicService>();
        
        Configure<AbpBackgroundWorkerQuartzOptions>(options =>
        {
            options.IsAutoRegisterEnabled = true;
        });
    }
    
    public override async Task OnApplicationInitializationAsync(
        ApplicationInitializationContext context)
    {
        await context.AddBackgroundWorkerAsync<TraktCalendarWorker>();
        await context.AddBackgroundWorkerAsync<TraktCollectionsWorker>();
        await context.AddBackgroundWorkerAsync<TraktWatchedWorker>();
        await context.AddBackgroundWorkerAsync<TraktWatchListWorker>();
    }
}
