using System.Collections.Generic;
using MediaInAction.VideoService.SeriesAliasNs;

namespace MediaInAction.VideoService.SeriesNs;

public class SeriesCreateDto 
{
    public string Name { get; set; }
    public int FirstAiredYear { get; set; }
    public List<SeriesAliasCreateDto> SeriesAliasCreateDtos { get; set; }
    public bool IsActive { get; set; }
    public string ImageName { get; set; }
}