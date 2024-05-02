using Volo.Abp.Application.Dtos;

namespace MediaInAction.VideoService.EpisodeNs.Dtos;

public class GetMyEpisodesInput : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}