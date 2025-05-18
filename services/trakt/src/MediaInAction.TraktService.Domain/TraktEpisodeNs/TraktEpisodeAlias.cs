using System;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities;

namespace MediaInAction.TraktService.TraktEpisodeNs;

public class TraktEpisodeAlias : Entity<Guid>
{
    public string IdType { get; set; }
    public string IdValue { get; set; }

    
    public TraktEpisodeAlias(Guid id, [NotNull]string idType, [NotNull]string idValue)
        : base(id)
    {
        IdType = idType;
        IdValue = idValue;
    }


}