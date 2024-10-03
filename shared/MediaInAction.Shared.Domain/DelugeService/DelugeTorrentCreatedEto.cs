using Volo.Abp.Domain.Entities.Events.Distributed;

namespace MediaInAction.Shared.Domain.DelugeService;

public class DelugeTorrentCreatedEto : EtoBase
{
    public string  Comment { get; set; }
    public bool IsSeed { get; set; }
    public string Hash { get; set; }
    public string Message { get; set; }
    public double Ratio { get; set; }
    public double CompleteTime { get; set; }
    public long Added { get; set; }
    public string Name { get; set; }
    public string DownloadLocation { get; set; }
    public string Label { get; set; }
    public bool Paused { get; set; }
}