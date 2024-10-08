﻿using System.Threading.Tasks;
using MediaInAction.TraktService.BG.JobArgs;
using MediaInAction.TraktService.Lib.TraktShowSeasonNs;
using Microsoft.Extensions.Logging;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.TraktService.BG.Jobs;
public class TraktGetSeasonsJob
        : AsyncBackgroundJob<TraktGetSeasonsArgs>, ITransientDependency
{
    private ITraktShowSeasonService _showSeasonService;
    
    public TraktGetSeasonsJob( ITraktShowSeasonService showSeasonService)
    {
        _showSeasonService = showSeasonService;
    }

    public override async Task ExecuteAsync(TraktGetSeasonsArgs args)
    {
        Logger.LogInformation("Background Job Do Episode Cleanup Starting");
        await _showSeasonService.DoEpisodeCleanup();
        Logger.LogInformation("Background Job Do Episode Cleanup Finished");
    }
}
