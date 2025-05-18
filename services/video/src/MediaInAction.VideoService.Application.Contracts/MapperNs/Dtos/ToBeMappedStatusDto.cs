using Volo.Abp.Application.Dtos;

namespace MediaInAction.VideoService.MapperNs.Dtos;

public class ToBeMappedStatusDto : EntityDto
{
    public int CountOfStatusToBeMapped { get; set; }
    public string ToBeMappedStatus { get; set; }
 
}