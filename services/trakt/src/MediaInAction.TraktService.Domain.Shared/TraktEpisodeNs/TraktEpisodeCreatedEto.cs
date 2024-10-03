﻿using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Events.Distributed;

namespace MediaInAction.TraktService.TraktEpisodeNs;

public class TraktEpisodeCreatedEto : EtoBase
{
    public string ShowSlug { get; set; }
    public int SeasonNum { get; set; }
    public int EpisodeNum { get; set; }
    public DateTime AiredDate { get; set; }
    public string EpisodeName  { get; set; }
    public List<TraktEpisodeAliasCreatedEto> TraktEpisodeCreateAliases { get; set; }
}