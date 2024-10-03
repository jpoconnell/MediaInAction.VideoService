using System;
using Volo.Abp.Application.Dtos;

namespace MediaInAction.TraktService.TraktEpisodeNs;

public class TraktEpisodeAliasCreateDto 
{
    public string IdType { get;  set; }
    public string IdValue { get;  set; }
}

