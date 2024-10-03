using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Events.Distributed;

namespace MediaInAction.TraktService.TraktShowNs;

public class TraktShowCreatedEto : EtoBase 
{
    public string Slug  { get; set; }
    public string Name { get; set; }
    public int FirstAiredYear { get; set; }
    public List<TraktShowAliasCreatedEto> TraktShowAliasCreatedEtos{ get; set; }
}