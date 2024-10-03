using System.Threading.Tasks;
using MediaInAction.TraktService.BG.JobArgs;
using MediaInAction.TraktService.Lib;
using Microsoft.Extensions.Logging;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.TraktService.BG.Jobs;

public class TraktWatchedJob
        : AsyncBackgroundJob<TraktWatchedArgs>, ITransientDependency  
{
  
    private ITraktService _traktService;
    
    public TraktWatchedJob( ITraktService traktService)
    {
        _traktService = traktService;
    }

    public override async Task ExecuteAsync(TraktWatchedArgs args)
    {
        Logger.LogInformation("Background Job TraktWatchedJob Starting");
        await _traktService.GetWatchedShows();
        await _traktService.GetWatchedMovies();
        Logger.LogInformation("Background Job TraktWatchedJob Finished");
    }
}