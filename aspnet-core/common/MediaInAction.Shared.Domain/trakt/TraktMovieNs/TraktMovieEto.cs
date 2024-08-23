using System.Collections.Generic;
using MediaInAction.Shared.Domain.Enums;
using MediaInAction.TraktService.TraktMovieNs;
using Volo.Abp.Domain.Entities.Events.Distributed;

namespace MediaInAction.Shared.Domain.trakt.TraktMovieNs
{
    public class TraktMovieEto : EtoBase
    {
        public string Name { get;  set; }
        public int FirstAiredYear { get; set; }
        public FileStatus MovieStatus { get; set; }
        public bool IsActive { get; set; }
        public List<TraktMovieAliasEto> MovieAliasEtos { get; set; }
    }
}