using Volo.Abp.Domain.Entities.Events.Distributed;

namespace MediaInAction.Shared.Domain.trakt.TraktEpisodeNs;

public class TraktEpisodeAliasUpdatedEto :  EtoBase
{
    public string IdType { get;  set; }
    public string IdValue { get;  set; }

    public TraktEpisodeAliasUpdatedEto ()
    {

    }
}

