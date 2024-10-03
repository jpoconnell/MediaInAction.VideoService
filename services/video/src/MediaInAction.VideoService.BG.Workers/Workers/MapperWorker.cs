using System.Threading.Tasks;
using MediaInAction.VideoService.MappingServicesNs;
using Microsoft.Extensions.Logging;
using Quartz;
using Volo.Abp.BackgroundWorkers.Quartz;

namespace MediaInAction.VideoService.BG.Workers;

public class MapperWorker : QuartzBackgroundWorkerBase
{
    private readonly IMediaMapper _mediaMapper;

    public MapperWorker( IMediaMapper mediaMapper)
    {
        JobDetail = JobBuilder.Create<MapperWorker>().WithIdentity(nameof(MapperWorker)).Build();
        Trigger = TriggerBuilder.Create().WithIdentity(nameof(MapperWorker)).WithSimpleSchedule(s=>s.WithIntervalInMinutes(15).RepeatForever().WithMisfireHandlingInstructionIgnoreMisfires()).Build();

        ScheduleJob = async scheduler =>
        {
            if (!await scheduler.CheckExists(JobDetail.Key))
            {
                await scheduler.ScheduleJob(JobDetail, Trigger);
            }
        };
        _mediaMapper = mediaMapper;
    }
    
    public override Task Execute(IJobExecutionContext context)
    {
        _mediaMapper.Map();
        Logger.LogInformation("Executed MediaMapperWorker..!");
        return Task.CompletedTask;
    }
}
