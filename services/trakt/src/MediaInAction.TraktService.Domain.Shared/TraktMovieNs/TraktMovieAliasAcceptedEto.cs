using Volo.Abp.Domain.Entities.Events.Distributed;

namespace MediaInAction.TraktService.TraktMovieNs;

    public class TraktMovieAliasAcceptedEto : EtoBase
    {
        public string IdType { get; set; }
        public string IdValue { get; set; }
    }

