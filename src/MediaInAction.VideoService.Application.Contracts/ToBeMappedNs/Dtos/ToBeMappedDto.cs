using MediaInAction.VideoService.Enums;
using Volo.Abp.Application.Dtos;

namespace MediaInAction.VideoService.ToBeMappedNs.Dtos;

public class ToBeMappedDto : EntityDto
{
    public string Alias { get; set; }
    public bool Processed { get; set; }
    public FromService FromService { get; set; }
    public string FromId { get; set; }
    public MediaType Type { get; set; }
    public int Tries { get; set; }
}