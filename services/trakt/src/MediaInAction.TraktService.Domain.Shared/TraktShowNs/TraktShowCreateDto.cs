using System.Collections.Generic;

namespace MediaInAction.TraktService.TraktShowNs;

public class TraktShowCreateDto 
{
    public string Slug  { get; set; }
    public string Name { get; set; }
    public int FirstAiredYear { get; set; }
    public string ImageName { get; set; }
    public TraktShowStatus Status { get; set; }
    public List<TraktShowAliasCreateDto> TraktShowCreateAliases{ get; set; }

}