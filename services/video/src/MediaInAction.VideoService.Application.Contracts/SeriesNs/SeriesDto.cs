using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace MediaInAction.VideoService.SeriesNs;

public class SeriesDto : EntityDto<Guid>
{
    public DateTime SeriesDate { get; set; } 
    public string SeriesName { get; set; } 
    public int FirstAiredYear { get; set; }
    public SeriesStatus SeriesStatus { get; set; }
    public List<SeriesAliasDto> SeriesAliasDtos { get; set; } = new();
}