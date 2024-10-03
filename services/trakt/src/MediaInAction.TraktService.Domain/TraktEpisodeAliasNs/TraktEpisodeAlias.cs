using System;
using Volo.Abp.Domain.Entities;

namespace MediaInAction.TraktService.TraktEpisodeAliasNs;

public class TraktEpisodeAlias : Entity<Guid>
{
    public string IdType { get; set; }
    public string IdValue { get; set; }
}
