using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;

namespace MediaInAction.Shared.Domain.trakt.TraktMovieNs;

[EventName("MediaInAction.TraktService.TraktMovie.Updated")]
public class TraktMovieUpdatedEto : EtoBase
{
    public string TraktId { get; set; }
    public string Slug { get; set; }
    public string Name { get; set; }
    public int FirstAiredYear { get; set; }
    public List<TraktMovieAliasUpdatedEto> TraktMovieAliasUpdatedEtos    { get; set; }
}