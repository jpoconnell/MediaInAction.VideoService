using System;
using System.Collections.Generic;
using MediaInAction.VideoService.Enums;
using MediaInAction.VideoService.SeriesAliasNs;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;

namespace MediaInAction.VideoService.SeriesNs;

public class SeriesCreateDto 
{
    public string Name { get; set; }
    public int FirstAiredYear { get; set; }
    public string imageName { get; set; }
    public List<SeriesAliasCreateDto> SeriesAliases { get; set; }
}