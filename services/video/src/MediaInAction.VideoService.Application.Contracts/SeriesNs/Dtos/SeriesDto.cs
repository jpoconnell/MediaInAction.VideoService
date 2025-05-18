using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace MediaInAction.VideoService.SeriesNs.Dtos;

public class SeriesDto : EntityDto<Guid>
{
    public Guid SeriesId { get; set; }
    public string SeriesName { get; set; } 
    public int FirstAiredYear { get; set; }
    public SeriesStatus SeriesStatus { get; set; }
    
    public DateTime CreateDate { get; set; } 
    public List<SeriesAliasDto> SeriesAliasDtos { get; set; } = new();
    public int Count { get; set; }
}