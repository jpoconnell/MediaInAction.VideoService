using Volo.Abp.Application.Dtos;

namespace MediaInAction.VideoService.SeriesNs.Dtos;

    public class GetSeriessInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
