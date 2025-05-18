using Volo.Abp.Application.Dtos;

namespace MediaInAction.VideoService.SeriesNs;

public class SeriesIsActiveDto : EntityDto
{
    public int CountOfInActiveSeries { get; set; }
    public string SeriesIsActive { get; set; }
 
}