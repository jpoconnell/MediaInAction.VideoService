using System;
using Volo.Abp.Application.Dtos;

namespace MediaInAction.VideoService.SeriesNs.Dtos;

[Serializable]
public class SeriesAliasLookupDto : EntityDto<Guid>
{
    public string IdValue { get; set; }
    public string IdType { get; set; }
}
