using Volo.Abp.Domain.Entities.Events.Distributed;

namespace MediaInAction.Shared.Domain.EmbyService;

public class EmbyShowAliasCreatedEto : EtoBase
{
    public string IdType { get; set; }
    public string IdValue  { get; set; }
    
}