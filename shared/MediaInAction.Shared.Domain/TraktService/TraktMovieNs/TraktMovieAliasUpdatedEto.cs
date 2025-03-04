using Volo.Abp.Domain.Entities.Events.Distributed;

namespace MediaInAction.Shared.Domain.TraktService.TraktMovieNs
{
    public class TraktMovieAliasUpdatedEto : EtoBase
    {
     
        public string IdType { get; set; }
        public string IdValue { get; set; }
    }
}