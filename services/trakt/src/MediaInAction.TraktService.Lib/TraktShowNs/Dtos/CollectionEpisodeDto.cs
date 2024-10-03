﻿using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace MediaInAction.TraktService.Lib.TraktShowNs.Dtos
{
    public class CollectionEpisodeDto : EntityDto<Guid>
    {
        public string ShowSlug { get;  set; }
        public int SeasonNum { get;  set; }
        public int EpisodeNum { get;  set; }
        public string EpisodeName { get; set; }
        public string AltEpisodeId { get; set; }
        public DateTime AiredDate { get; set; }
        public List<CollectionEpisodeAliasDto> CollectionEpisodeAliasDtos { get; set; }
        
    }
}