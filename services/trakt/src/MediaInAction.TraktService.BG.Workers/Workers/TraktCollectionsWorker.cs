﻿using System.Threading.Tasks;
using MediaInAction.TraktService.Lib;
using Microsoft.Extensions.Logging;
using Quartz;
using Volo.Abp.BackgroundWorkers.Quartz;

namespace MediaInAction.TraktService.BG.Workers;

public class TraktCollectionsWorker : QuartzBackgroundWorkerBase
{
    private readonly ITraktService _traktService;

    public TraktCollectionsWorker( ITraktService traktService)
    {
        JobDetail = JobBuilder.Create<TraktCollectionsWorker>().WithIdentity(nameof(TraktCollectionsWorker)).Build();
        Trigger = TriggerBuilder.Create().WithIdentity(nameof(TraktCollectionsWorker)).WithSimpleSchedule(s=>s.WithIntervalInHours(24).RepeatForever().WithMisfireHandlingInstructionIgnoreMisfires()).Build();

        ScheduleJob = async scheduler =>
        {
            if (!await scheduler.CheckExists(JobDetail.Key))
            {
                await scheduler.ScheduleJob(JobDetail, Trigger);
            }
        };
        _traktService = traktService;
    }
    
    public override Task Execute(IJobExecutionContext context)
    {
        Logger.LogInformation("Background Worker TraktCollectionsWorker Starting..!");
        _traktService.GetShowCollection();
        Logger.LogInformation("Executed GetShowCollection..!");
        _traktService.GetMovieCollection();
        Logger.LogInformation("Executed GetMovieCollection..!");
        Logger.LogInformation("Background Worker TraktCollectionsWorker Complete");
        return Task.CompletedTask;
    }
}
