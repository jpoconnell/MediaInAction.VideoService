using System;
using System.Collections.Generic;

namespace MediaInAction.TraktService.TraktEpisodeNs;

public class TraktEpisodeCreateDto 
{
    public string ShowSlug { get; set; }
    public int SeasonNum { get; set; }
    public int EpisodeNum { get; set; }
    public DateTime AiredDate { get; set; }
    public string EpisodeName  { get; set; }
    public List<TraktEpisodeAliasCreateDto> TraktEpisodeCreateAliases { get; set; }
}