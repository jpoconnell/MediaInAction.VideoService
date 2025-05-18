using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace MediaInAction.VideoService.MapperNs.Dtos;

public class ToBeMappedDashboardDto: EntityDto
{
    public List<ToBeMappedStatusDto> ToBeMappedStatusDto { get; set; }

}