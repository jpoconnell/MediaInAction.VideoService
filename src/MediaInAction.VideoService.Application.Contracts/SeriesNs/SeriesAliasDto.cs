using System;
using Volo.Abp.Application.Dtos;

namespace MediaInAction.VideoService.SeriesNs;

public class SeriesAliasDto : EntityDto<Guid>
{
    public Guid SeriesId { get; set; }
    public string IdType { get; set; }
    public string IdValue { get; set; }
}