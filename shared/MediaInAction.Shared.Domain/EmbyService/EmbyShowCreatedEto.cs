using System;
using System.Collections.Generic;
using MediaInAction.Shared.Domain.VideoService.EpisodeAliasNs;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;

namespace MediaInAction.Shared.Domain.EmbyService;

[Serializable]
[EventName("MediaInAction.Show.Created")]
public class EmbyShowCreatedEto : EtoBase
{
    public string SeriesId { get; set; }
    public string ShowName { get; set; }
    public int FirstAiredYear { get; set; }
    public List<EmbyShowAliasCreatedEto> EmbyShowAliasCreatedEtos { get; set; }
}


