using Volo.Abp.Domain.Entities.Events.Distributed;

namespace MediaInAction.Shared.Domain.trakt.TraktEpisodeNs
{
    public class TraktEpisodeAliasEto : EtoBase
    {
        public string IdType { get; set; }
        public string IdValue { get; set; }
    }
}