namespace MediaInAction.TraktService.TraktMovieNs.Dtos;

public class GetTraktMovieListDto
{
    public string Sorting { get; set; }
    public int SkipCount { get; set; }
    public int MaxResultCount { get; set; }
    public string Filter { get; set; }
}