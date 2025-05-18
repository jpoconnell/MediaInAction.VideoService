using System.Threading.Tasks;

namespace MediaInAction.DelugeService.Bg.DelugeTorrentNs;

public interface IDelugeTorrentService
{
    Task GetTorrentCollection();
}

