using System;
using System.Collections.Generic;
using MediaInAction.VideoService.SeriesAliasNs;
using Volo.Abp.Application.Dtos;

namespace MediaInAction.VideoService.SeriesNs;

public class SeriesDto : EntityDto<Guid>
{
    public string Name { get; set; }
    public int FirstAiredYear { get; set; }
    public List<SeriesAliasDto> SeriesAliasDtos { get; set; }
    public bool IsActive { get; set; }
    public string ImageName { get; set; }
}