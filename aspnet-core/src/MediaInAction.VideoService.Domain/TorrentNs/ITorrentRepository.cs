using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.Enums;
using Volo.Abp.Domain.Repositories;

namespace MediaInAction.VideoService.TorrentsNs
{
    public interface ITorrentRepository : IRepository<Torrent, Guid>
    {
        Task<Torrent> FindByHash(string hash);

        Task<List<Torrent>> GetUnMapped();

        Task<List<Torrent>> GetTorrentStatus(FileStatus status);
        Task<List<Torrent>> GetMapped(bool mapped);
    }
}
