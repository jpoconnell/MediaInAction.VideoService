using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;

namespace MediaInAction.Shared.Domain.TraktService.TraktShowNs;

[EventName("MediaInAction.TraktShow.Acknowledge")]
public class TraktShowAcknowledgeEto : EtoBase
{
    public string TraktId  { get; set; }
    public string Slug  { get; set; }
    public string Name  { get; set; }
    public int Year { get; set; }
}