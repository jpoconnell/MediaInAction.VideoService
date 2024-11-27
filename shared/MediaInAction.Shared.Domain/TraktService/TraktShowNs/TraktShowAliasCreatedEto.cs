using Volo.Abp.Domain.Entities.Events.Distributed;

namespace MediaInAction.Shared.Domain.TraktService.TraktShowNs
{
    public class TraktShowAliasCreatedEto : EtoBase
    {

        public string IdType { get; set; }
        public string IdValue { get; set; }
    }
}