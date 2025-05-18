using System.Collections.Generic;
using JetBrains.Annotations;


namespace MediaInAction.VideoService.MovieNs;

public class MovieCreateDto 
{
    public string Name { get; set; }
    public int FirstAiredYear { get; set; }
    [CanBeNull] public string ImageName { get; set; }
    public List<MovieAliasCreateDto> MovieAliases { get; set; }

    public MovieStatus MovieStatus { get; set; }
}