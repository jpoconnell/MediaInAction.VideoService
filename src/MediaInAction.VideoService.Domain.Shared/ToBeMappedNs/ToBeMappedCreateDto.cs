using MediaInAction.VideoService.Enums;

namespace MediaInAction.VideoService.ToBeMappedNs;

public class ToBeMappedCreateDto 
{
    public string Alias { get; set; }
    public bool Processed { get; set; }
    public FromService FromService { get; set; }
    public string FromId { get; set; }
    public MediaType Type { get; set; }
    public int Tries { get; set; }
}