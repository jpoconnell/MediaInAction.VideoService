using System;
using System.Collections.Generic;
using MediaInAction.EmbyService.EmbyEpisodeAliasNs;

namespace MediaInAction.EmbyService.EmbyEpisodeNs;


public class EmbyEpisodeCreateDto 
{
    public Guid EmbySeriesId  { get; set; }
    public int SeasonNum { get; set; }
    public int EpisodeNum { get; set; }
    public DateTime AiredDate { get; set; }
    public string EpisodeName { get; set; }
    public EmbyEpisodeStatus Status { get; set; }
    public List<EmbyEpisodeAliasCreateDto> EmbyEpisodeAliasCreateDto { get; set; }
}