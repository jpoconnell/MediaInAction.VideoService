using System.Collections.Generic;
using MediaInAction.VideoService.Enums;


namespace MediaInAction.VideoService.MovieNs;

public class MovieCreateDto 
{
    public string Name { get; set; }
    public int FirstAiredYear { get; set; }
    public string ImageName { get; set; }
    public List<MovieAliasCreateDto> MovieAliases { get; set; }
    public bool IsActive { get; set; }
    public MediaStatus MediaStatus { get; set; }
}