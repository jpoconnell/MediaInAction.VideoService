namespace MediaInAction.Shared.Domain.VideoService.EpisodeNs;

public enum DownloadStatusType
{
    New,
    Indexed,
    Torrent,
    Compressed,
    UnCompressed,
    Complete,
    Watched,
    Move
}
