using System.Threading.Tasks;
using AutoMapper;
using MediaInAction.VideoService.BG.Workers;
using MediaInAction.VideoService.EntityFrameworkCore;
using MediaInAction.VideoService.EpisodeNs;
using MediaInAction.VideoService.MappingServicesNs;
using MediaInAction.VideoService.MediaMatchingServicesNs;
using MediaInAction.VideoService.MovieAliasNs;
using MediaInAction.VideoService.MovieNs;
using MediaInAction.VideoService.ParserNs;
using MediaInAction.VideoService.SeriesAliasNs;
using MediaInAction.VideoService.SeriesNs;
using MediaInAction.VideoService.ToBeMappedsNs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.BackgroundWorkers.Quartz;
using Volo.Abp.Modularity;
using Volo.Abp.Quartz;

namespace MediaInAction.VideoService.BG;

[DependsOn(
    typeof(AbpBackgroundWorkersQuartzModule),
    typeof(VideoServiceLibModule),
    typeof(VideoServiceDomainSharedModule),
    typeof(VideoServiceEntityFrameworkCoreModule) 
)]
public class VideoServiceBgWorkerModule : AbpModule
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
        context.Services.TryAddSingleton<IMapper,Mapper>();
        context.Services.TryAddSingleton<IEpisodeService,EpisodeService>();
        context.Services.TryAddSingleton<IToBeMappedService,ToBeMappedService>();
        
        context.Services.TryAddSingleton<ISeriesService,SeriesService>();
        context.Services.TryAddSingleton<ISeriesAliasService,SeriesAliasService>();   
        context.Services.TryAddSingleton<ISeriesMatchingService,SeriesMatchingService>();
        context.Services.TryAddSingleton<IMediaMapper, MediaMapper>();
        context.Services.TryAddSingleton<IMovieService,MovieService>();
        context.Services.TryAddSingleton<IMovieAliasRepository,EfCoreMovieAliasRepository>();
        
        context.Services.TryAddSingleton<IMovieAliasLibService,MovieAliasLibService>();
        context.Services.TryAddSingleton<IMovieMatchingService,MovieMatchingService>();
        context.Services.TryAddSingleton<IParserService,ParserService>();
        Configure<AbpBackgroundWorkerQuartzOptions>(options =>
        {
            options.IsAutoRegisterEnabled = true;
        });
    }
    
    public override async Task OnApplicationInitializationAsync(
        ApplicationInitializationContext context)
    {
        await context.AddBackgroundWorkerAsync<MapperWorker>();
    }
}