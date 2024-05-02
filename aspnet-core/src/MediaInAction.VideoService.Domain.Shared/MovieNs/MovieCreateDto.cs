using System;
using System.Collections.Generic;
using MediaInAction.VideoService.Enums;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;

namespace MediaInAction.VideoService.MovieNs;

public class MovieCreateDto 
{
    public string Name { get; set; }
    public int FirstAiredYear { get; set; }
    public string ImageName { get; set; }
    public List<MovieAliasCreateDto> MovieAliases { get; set; }
}