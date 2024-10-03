using System;
using System.Collections.Generic;
using MediaInAction.Shared.Domain.Enums;
using MediaInAction.TraktService.TraktEpisodeAliasNs;
using Volo.Abp.Domain.Entities.Auditing;

namespace MediaInAction.TraktService.TraktEpisodeNs;

public class TraktEpisode : AuditedAggregateRoot<Guid>
{
    public string ShowSlug { get; set; }
    public int SeasonNum { get; set; }
    public int EpisodeNum { get; set; }
    public string EpisodeName { get; set; }
    public DateTime AiredDate { get; set; }
    public List<TraktEpisodeAlias > TraktEpisodeAliases { get; set; }
    public FileStatus TraktStatus { get; set; }
    public bool Changed { get; set; }
}