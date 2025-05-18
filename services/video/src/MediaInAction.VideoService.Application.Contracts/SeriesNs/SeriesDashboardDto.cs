using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace MediaInAction.VideoService.SeriesNs;

public class SeriesDashboardDto: EntityDto
{
    public List<SeriesStatusDto> SeriesStatusDto { get; set; }
    public List<SeriesIsActiveDto> SeriesIsActiveDto { get; set; }
}