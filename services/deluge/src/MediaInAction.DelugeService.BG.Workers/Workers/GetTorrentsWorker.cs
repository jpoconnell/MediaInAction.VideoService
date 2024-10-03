using System.Threading.Tasks;
using MediaInAction.DelugeService.Bg.DelugeTorrentNs;
using Microsoft.Extensions.Logging;
using Quartz;
using Volo.Abp.BackgroundWorkers.Quartz;

namespace MediaInAction.DelugeService.Bg.Workers;

public class GetTorrentsWorker : QuartzBackgroundWorkerBase
{
    private readonly IDelugeTorrentService _torrentService;
    
    public GetTorrentsWorker(IDelugeTorrentService torrentService)
    {
        JobDetail = JobBuilder.Create<GetTorrentsWorker>().WithIdentity(nameof(GetTorrentsWorker)).Build();
        Trigger = TriggerBuilder.Create().WithIdentity(nameof(GetTorrentsWorker)).WithSimpleSchedule(s=>s.WithIntervalInMinutes(1).RepeatForever().WithMisfireHandlingInstructionIgnoreMisfires()).Build();

        ScheduleJob = async scheduler =>
        {
            if (!await scheduler.CheckExists(JobDetail.Key))
            {
                await scheduler.ScheduleJob(JobDetail, Trigger);
            }
        };

        _torrentService = torrentService;
    }

    public override  Task Execute(IJobExecutionContext context)
    {
         _torrentService.GetTorrentCollection();
        Logger.LogInformation("Executed GetTorrents Worker..!");
        return Task.CompletedTask;
    }
}
