﻿using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;

namespace MediaInAction.Shared.Domain.trakt.TraktShowNs;

[EventName("MediaInAction.TraktShow.Updated")]
public class TraktShowUpdatedEto : EtoBase
{
    public string TraktId  { get; set; }
    public string Slug  { get; set; }
    public string Name { get; set; }
    public int FirstAiredYear { get; set; }
    public List<TraktShowAliasUpdatedEto> TraktShowAliasUpdatedEtos { get; set; }
}