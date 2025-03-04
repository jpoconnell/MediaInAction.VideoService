
using System;

namespace MediaInAction.VideoService.SeriesNs;

public class SeriesAliasCreateDto 
{
    public Guid SeriesId { get; set; }
    public string IdType { get; set; }
    public string IdValue { get; set; }
}