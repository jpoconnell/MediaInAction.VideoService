using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediaInAction.VideoService.Enums;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.TorrentsNs
{
    public interface ITorrentRepository : IRepository<Torrent, Guid>
    {
        Task<Torrent> FindByHash(string hash);

        Task<List<Torrent>> GetUnMapped();

        Task<List<Torrent>> GetTorrentStatus(FileStatus status);
        Task<List<Torrent>> GetMapped(bool mapped);

        Task<List<Torrent>> GetListPagedAsync(ISpecification<Torrent> specification, 
            int inputSkipCount, 
            int inputMaxResultCount, 
            string empty,
            CancellationToken cancellationToken = default);
    }
}
