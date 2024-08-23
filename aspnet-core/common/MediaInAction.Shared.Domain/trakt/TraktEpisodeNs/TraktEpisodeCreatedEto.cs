using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;

namespace MediaInAction.Shared.Domain.trakt.TraktEpisodeNs;

[EventName("MediaInAction.TraktEpisode.Created")]
public class TraktEpisodeCreatedEto : EtoBase
{
    public string TraktId  { get; set; }
    public string ShowSlug { get; set; }
    public int SeasonNum { get; set; }
    public int EpisodeNum { get; set; }
    public DateTime AiredDate { get; set; }
    public string EpisodeName  { get; set; }
    public List<TraktEpisodeAliasCreatedEto> TraktEpisodeAliasCreatedEtos { get; set; }
   
}