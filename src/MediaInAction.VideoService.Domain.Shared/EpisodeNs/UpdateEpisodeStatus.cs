using System;

namespace MediaInAction.VideoService.EpisodeNs;

public class UpdateEpisodeStatus
{
    public Guid Id { get; set; }
    public EpisodeStatus EpisodeStatus  { get; set; }
}