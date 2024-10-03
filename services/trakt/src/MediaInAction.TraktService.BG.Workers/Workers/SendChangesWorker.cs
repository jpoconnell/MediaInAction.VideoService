using System.Threading.Tasks;
using MediaInAction.TraktService.Lib.TraktEpisodeNs;
using MediaInAction.TraktService.Lib.TraktMovieNs;
using MediaInAction.TraktService.Lib.TraktShowNs;
using Microsoft.Extensions.Logging;
using Quartz;
using Volo.Abp.BackgroundWorkers.Quartz;

namespace MediaInAction.TraktService.BG.Workers;

public class SendChangesWorker : QuartzBackgroundWorkerBase
{
    private readonly ITraktShowLibService _traktShowLibService;
    private readonly ITraktEpisodeLibService _traktEpisodeLibService;
    private readonly ITraktMovieLibService _traktMovieLibService;

    public SendChangesWorker( ITraktShowLibService traktShowLibService,
        ITraktEpisodeLibService traktEpisodeLibService,
        ITraktMovieLibService traktMovieLibService)
    {
        JobDetail = JobBuilder.Create<SendChangesWorker>().WithIdentity(nameof(SendChangesWorker)).Build();
        Trigger = TriggerBuilder.Create().WithIdentity(nameof(SendChangesWorker)).WithSimpleSchedule(s=>s.WithIntervalInHours(12).RepeatForever().WithMisfireHandlingInstructionIgnoreMisfires()).Build();

        ScheduleJob = async scheduler =>
        {
            if (!await scheduler.CheckExists(JobDetail.Key))
            {
                await scheduler.ScheduleJob(JobDetail, Trigger);
            }
        };
        _traktShowLibService = traktShowLibService;
        _traktEpisodeLibService = traktEpisodeLibService;
        _traktMovieLibService = traktMovieLibService;
    }
    
    public override Task Execute(IJobExecutionContext context)
    {
        Logger.LogInformation("Background Worker Resend Starting..!");
       // _traktShowLibService.ResendUnAcceptedShowsList();
        
        Logger.LogInformation("Executed Resend Movies..!");
        return Task.CompletedTask;
    }
}
