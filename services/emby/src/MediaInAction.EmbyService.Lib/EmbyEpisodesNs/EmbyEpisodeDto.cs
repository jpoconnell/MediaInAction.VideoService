using System;
using System.Collections.Generic;

namespace MediaInAction.EmbyService.EmbyEpisodesNs;

public class EmbyEpisodeDto 
{
    public string EmbyId { get; set; }
    public string Name { get; set; }
    public Guid ShowId { get; set; }
    public string ShowName { get; set; }
    public string EmbySeriesId { get; set; }
    public string EmbySeasonId { get; set; }
    public int SeasonNum { get; set; }
    public int EpisodeNum { get; set; }
    public DateTime AiredDate { get; set; }
    
    public List<EmbyEpisodeAliasDto> EpisodeAliases { get; set; }
}
