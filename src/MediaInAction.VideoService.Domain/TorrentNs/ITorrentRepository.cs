using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace MediaInAction.VideoService.TorrentsNs
{
    public interface ITorrentRepository : IRepository<Torrent, Guid>
    {
        Task<Torrent> FindByHash(string hash);

        Task<List<Torrent>> GetUnMapped();

    }
}
