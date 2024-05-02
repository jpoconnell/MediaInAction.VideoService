using MediaInAction.VideoService.Enums;
using Volo.Abp.Application.Dtos;

namespace MediaInAction.VideoService.TorrentNs.Dtos;

public class GetTorrentsInput : PagedAndSortedResultRequestDto
{
    public bool Processed { get; set; }
    public bool IsMapped { get; set; }
    public FileStatus TorrentStatus { get; set; }
}