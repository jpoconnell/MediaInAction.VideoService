using System;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities;

namespace MediaInAction.TraktService.TraktMovieNs;

public class TraktMovieAlias : Entity<Guid>
{
    public string IdType { get; set; }
    public string IdValue { get; set; }

    
    public TraktMovieAlias(Guid id, [NotNull]string idType, [NotNull]string idValue)
        : base(id)
    {
        IdType = idType;
        IdValue = idValue;
    }
}
