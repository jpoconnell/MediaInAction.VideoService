using Volo.Abp.Domain.Entities.Events.Distributed;

namespace  MediaInAction.TraktService.TraktShowNs;
public class TraktShowAliasCreatedEto : EtoBase
{
    public string IdType { get; set; }
    public string IdValue { get; set; }
}

