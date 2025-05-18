using System.Collections.Generic;
using MediaInAction.EmbyService.EmbyMovieAliasNs;

namespace MediaInAction.EmbyService.EmbyMovieNs;

public class EmbyMovieCreateDto 
{
    public string EmbyId { get; set; }
    public string Name { get; set; }
    public int FirstAiredYear { get; set; }
    public EmbyMovieStatus Status { get; set; }
    
    public List<EmbyMovieAliasCreateDto> EmbyMovieCreateAliasesDto { get; set; }
    
}