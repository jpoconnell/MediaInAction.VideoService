﻿using System;
using System.Collections.Generic;
using MediaInAction.Shared.Domain.VideoService.EpisodeAliasNs;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;

namespace MediaInAction.Shared.Domain.VideoService.EpisodeNs;

[Serializable]
[EventName("MediaInAction.Episode.Created")]
public class EpisodeCreatedEto : EtoBase
{
    public string SeriesId { get; set; }
    public string EpisodeId { get; set; }
    public int SeasonNum { get; set; }
    public int EpisodeNum { get; set; }
    public DateTime AiredDate { get; set; }
    public List<EpisodeAliasCreatedEto> EpisodeAliasCreatedEtos { get; set; }
}


