using Volo.Abp.BackgroundJobs;

namespace MediaInAction.TraktService.BG.JobArgs
{
    [BackgroundJobName("traktmoviecollection")]
    public class TraktGetCollectionArgs 
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}