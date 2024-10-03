using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus;

namespace MediaInAction.Shared.Domain.EmbyService;

[EventName("MediaInAction.EmbyShow.Accepted")]
public class EmbyShowAcceptedEto : EtoBase
{
    public string EmbyShowId { get; set; }
    public string ShowName { get; set; }
    public int FirstAiredYear { get; set; }
}