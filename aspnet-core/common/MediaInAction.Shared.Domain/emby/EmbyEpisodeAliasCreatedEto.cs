﻿using Volo.Abp.Domain.Entities.Events.Distributed;

namespace MediaInAction.Shared.Domain.emby;

public class EmbyEpisodeAliasCreatedEto : EtoBase
{
    public string IdType { get; set; }
    public string IdValue  { get; set; }
    
}