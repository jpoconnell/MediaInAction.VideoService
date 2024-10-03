using Volo.Abp.Application.Dtos;

namespace MediaInAction.DelugeService.DelugeTorrentNs.Dtos
{
    public class GetTorrentListDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}