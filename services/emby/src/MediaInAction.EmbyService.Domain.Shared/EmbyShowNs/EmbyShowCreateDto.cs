using System.Collections.Generic;
using MediaInAction.EmbyService.EmbyShowAliasesNs;

namespace MediaInAction.EmbyService.EmbyShowNs;

public class EmbyShowCreateDto 
{
    public string Id { get; set; }
    public string Server { get; set; }
    public string EmbyId { get; set; }
    public string Name { get; set; }
    public int FirstAiredYear { get; set; }
    public EmbyShowStatus Status { get; set; }
    
    public List<EmbyShowAliasCreateDto> EmbyShowCreateAliases { get; set; }
}