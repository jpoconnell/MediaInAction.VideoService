﻿using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;

namespace MediaInAction.Shared.Domain.TraktService.TraktMovieNs;

[EventName("MediaInAction.TraktService.TraktMovie.Created")]
public class TraktMovieCreatedEto : EtoBase
{
    public string TraktId { get; set; }
    public string Slug { get; set; }
    public string Name { get; set; }
    public int FirstAiredYear { get; set; }
    public List<TraktMovieAliasCreatedEto> TraktMovieAliasCreatedEtos { get; set; }
}