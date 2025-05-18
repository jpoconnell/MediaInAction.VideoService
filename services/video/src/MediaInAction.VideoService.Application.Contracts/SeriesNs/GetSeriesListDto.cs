using System;
using Volo.Abp.Application.Dtos;

namespace MediaInAction.VideoService.SeriesNs;

[Serializable]
public class GetSeriesListDto : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}
