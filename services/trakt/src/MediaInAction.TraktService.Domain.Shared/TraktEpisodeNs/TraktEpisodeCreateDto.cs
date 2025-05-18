using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace MediaInAction.TraktService.TraktEpisodeNs;

public class TraktEpisodeCreateDto 
{
    public string Slug { get; set; }
    public int SeasonNum { get; set; }
    public int EpisodeNum { get; set; }
    public DateTime? AiredDate { get; set; }
    [CanBeNull] public string EpisodeName  { get; set; }
    
    public TraktEpisodeStatus Status  { get; set; }
    public List<TraktEpisodeAliasCreateDto> TraktEpisodeCreateAliases { get; set; }
    
}