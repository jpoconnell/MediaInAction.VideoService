using System.Collections.Generic;
using MediaInAction.Shared.Domain.Enums;
using Volo.Abp.Domain.Entities.Events.Distributed;

namespace MediaInAction.TraktService.TraktShowNs;

public class TraktShowAcceptedEto : EtoBase
{
  
    public string Slug { get; set; }
    public string Name { get; set; }
    public int FirstAiredYear { get; set; }
    public List<TraktShowAliasAcceptedEto> TraktShowAliasAcceptedEtos { get; set; }
    public bool Changed { get; set; }
    public bool Accepted { get; set; }
    public FileStatus TraktStatus { get; set; }
}