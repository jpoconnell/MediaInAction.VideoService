using System;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities;

namespace MediaInAction.TraktService.TraktShowNs;

public class TraktShowAlias : Entity<Guid>
{
    public Guid ShowId { get; set; }
    public string IdType { get; set; }
    public string IdValue { get; set; }

    
    public TraktShowAlias(Guid id, [NotNull]string idType, [NotNull]string idValue)
        : base(id)
    {
        IdType = idType;
        IdValue = idValue;
    }
}
