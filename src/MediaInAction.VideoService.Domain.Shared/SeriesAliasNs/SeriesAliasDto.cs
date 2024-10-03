
using System;

namespace MediaInAction.VideoService.SeriesAliasNs;

public class SeriesAliasDto 
{
    public Guid SeriesId { get; set; }
    public string IdType { get; set; }
    public string IdValue { get; set; }
}