﻿using MediaInAction.Shared.Domain.TraktService.TraktShowNs;
using Volo.Abp.Domain.Entities.Events.Distributed;

namespace MediaInAction.Shared.Domain.TraktService.TraktShowSeasonNs
{
    public class TraktShowSeasonEto : EtoBase
    {
        public TraktShowEto Show { get; set; }
        public int Season { get; set; }
    }
}