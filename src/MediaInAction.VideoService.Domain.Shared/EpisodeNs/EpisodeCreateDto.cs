using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using MediaInAction.VideoService.EpisodeAliasNs;

namespace MediaInAction.VideoService.EpisodeNs;

public class EpisodeCreateDto 
{
    public string SeriesName { get; set; }
    public string SeriesYear { get; set; }
    public Guid? SeriesId { get; set; }
    public int SeasonNum { get; set; }
    public int EpisodeNum { get; set; }
    public DateTime? AiredDate { get; set; }
    [CanBeNull] public string EpisodeName { get; set; }
    public List<EpisodeAliasCreateDto> EpisodeCreateAliases { get; set; }
}


