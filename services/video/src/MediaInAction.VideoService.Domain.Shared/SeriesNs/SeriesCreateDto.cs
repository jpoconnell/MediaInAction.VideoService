using System.Collections.Generic;
using MediaInAction.Shared.Domain.Enums;

namespace MediaInAction.VideoService.SeriesNs;

public class SeriesCreateDto 
{
    public string Name { get; set; }
    public int FirstAiredYear { get; set; }
  
    public SeriesStatus SeriesStatus { get; set; }
    public string ImageName { get; set; }
    
    public List<SeriesAliasCreateDto> SeriesAliasCreateDtos { get; set; }
}