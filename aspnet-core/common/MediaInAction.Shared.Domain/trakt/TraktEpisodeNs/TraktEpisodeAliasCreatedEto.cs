using Volo.Abp.Domain.Entities.Events.Distributed;

namespace MediaInAction.Shared.Domain.trakt.TraktEpisodeNs
{
    public class TraktEpisodeAliasCreatedEto : EtoBase
    {
        public string IdType { get; set; }
        public string IdValue { get; set; }
    }
}