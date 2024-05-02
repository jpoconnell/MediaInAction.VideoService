using Volo.Abp.Application.Dtos;

namespace   MediaInAction.VideoService.MovieNs.Dtos;

public class GetMoviesInput : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }
}