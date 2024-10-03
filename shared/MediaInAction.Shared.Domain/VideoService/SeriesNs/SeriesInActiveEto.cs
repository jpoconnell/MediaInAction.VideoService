using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Events.Distributed;

namespace MediaInAction.Shared.Domain.VideoService.SeriesNs;

public class SeriesInactiveEto : EtoBase
{
    public string Name { get; set; }
    public int FirstAiredYear { get; set; }
    public string imageName { get; set; }
    public List<SeriesAliasCreatedEto> SeriesAliases { get; set; }
}