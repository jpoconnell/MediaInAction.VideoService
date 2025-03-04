using Volo.Abp.Domain.Entities.Events.Distributed;

namespace MediaInAction.Shared.Domain.VideoService.SeriesNs
{
    public class SeriesAliasCreatedEto : EtoBase
    {
        public string IdType { get; set; }
        public string IdValue { get; set; }
    }
}
