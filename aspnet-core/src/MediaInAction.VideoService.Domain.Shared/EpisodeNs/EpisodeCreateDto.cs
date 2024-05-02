using System;
using System.Collections.Generic;
using MediaInAction.VideoService.EpisodesAliasNs;

namespace MediaInAction.VideoService.EpisodeNs;

public class EpisodeCreateDto 
{
    public string SeriesName { get; set; }
    public string SeriesYear { get; set; }
    public int SeasonNum { get; set; }
    public int EpisodeNum { get; set; }
    public DateTime? AiredDate { get; set; }
    public List<EpisodeAliasCreateDto> EpisodeAliases { get; set; }
}


