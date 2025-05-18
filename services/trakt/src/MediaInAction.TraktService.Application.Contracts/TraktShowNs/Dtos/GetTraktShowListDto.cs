namespace MediaInAction.TraktService.TraktShowNs.Dtos;

public class GetTraktShowListDto
{
    public string Sorting { get; set; }
    public int SkipCount { get; set; }
    public int MaxResultCount { get; set; }
    public string Filter { get; set; }
}