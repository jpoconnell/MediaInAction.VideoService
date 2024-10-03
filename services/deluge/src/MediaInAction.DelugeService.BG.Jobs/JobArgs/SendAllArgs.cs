using Volo.Abp.BackgroundJobs;

namespace MediaInAction.DelugeService.Bg.JobArgs
{
    [BackgroundJobName("delugeSendAll")]
    public class SendAllArgs
    {
    public string ApiKey { get; set; }
    }
}