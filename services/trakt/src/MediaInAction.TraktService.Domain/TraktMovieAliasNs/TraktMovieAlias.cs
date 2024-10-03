using System;
using Volo.Abp.Domain.Entities;

namespace MediaInAction.TraktService.TraktMovieAliasNs;

public class TraktMovieAlias : Entity<Guid>
{
    public string IdType { get; set; }
    public string IdValue { get; set; }
}
