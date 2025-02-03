using Volo.Abp.Application.Dtos;

namespace MediaInAction.VideoService.SeriesNs;

public class SeriesStatusDto : EntityDto
{
    public int CountOfStatusSeries { get; set; }
    public string SeriesStatus { get; set; }
 
}