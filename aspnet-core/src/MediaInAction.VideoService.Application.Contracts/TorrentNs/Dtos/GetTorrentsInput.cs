using MediaInAction.VideoService.Enums;

namespace MediaInAction.VideoService.TorrentNs.Dtos;

public class GetTorrentsInput 
{
    public bool Processed { get; set; }
    public bool IsMapped { get; set; }
    public FileStatus TorrentStatus { get; set; }
}