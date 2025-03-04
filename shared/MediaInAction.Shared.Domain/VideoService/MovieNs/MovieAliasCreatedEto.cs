using Volo.Abp.Domain.Entities.Events.Distributed;

namespace MediaInAction.Shared.Domain.VideoService.MovieNs
{
    public class MovieAliasCreatedEto : EtoBase
    {
        public string IdType { get; set; }
        public string IdValue { get; set; }
    }
}
