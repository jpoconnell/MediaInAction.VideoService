﻿using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Events.Distributed;

namespace MediaInAction.Shared.Domain.TraktService.TraktEpisodeNs
{
    public  class TraktEpisodeEto : EtoBase
    {
        public string ShowSlug { get;  set; }
        public int SeasonNum { get;  set; }
        public int EpisodeNum { get;  set; }
        public string EpisodeName { get; set; }
        public string AltEpisodeId { get; set; }
        public DateTime AiredDate { get; set; }
        public List<TraktEpisodeAliasEto> EpisodeAliasEtos { get; set; }
        
    }
}