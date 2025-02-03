using Volo.Abp.Application.Dtos;

namespace MediaInAction.VideoService.SeriesNs;

public class SeriesYearDto : EntityDto
{
    public int CountOfYearSeries { get; set; }
    public string SeriesYear { get; set; }
 
}