using System;
using System.Collections.Generic;
using MediaInAction.EmbyService.EmbyMovieAliasesNs.Dtos;
using Volo.Abp.Domain.Entities.Auditing;

namespace MediaInAction.EmbyService.EmbyMovieNs;

public class EmbyMovie:  AuditedAggregateRoot<Guid>
{
    public string Name { get; set; }
    public string ServerId { get; set; }
    public int ProductionYear { get; set; }
    public int FirstAiredYear { get; set; }
    
    public List<EmbyMovieAlias> EmbyMovieAliases { get; set; }
}
