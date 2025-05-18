using Volo.Abp.BackgroundJobs;

namespace MediaInAction.EmbyService.Bg.JobArgs
{
    [BackgroundJobName("embySendAll")]
    public class EmbySendAllArgs 
    {
        public string CurrentLocation { get; set; }
    }
}