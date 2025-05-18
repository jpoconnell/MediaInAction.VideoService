using System;
using System.Collections.Generic;
using MediaInAction.VideoService.SeriesNs;
using Volo.Abp.Application.Dtos;

namespace MediaInAction.VideoService.MapperNs.Dtos;

public class MapperDto : EntityDto<Guid>
{
    public DateTime SeriesDate { get; set; } 
    public string SeriesName { get; set; } 
    public int FirstAiredYear { get; set; }
    public SeriesStatus SeriesStatus { get; set; }
    public List<SeriesAliasDto> SeriesAliasDtos { get; set; } = new();
}