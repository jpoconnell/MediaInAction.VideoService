using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediaInAction.DelugeService.DelugeTorrentNs;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Specifications;

namespace MediaInAction.DelugeService.DelugeTorrentsNs;

public interface IDelugeTorrentRepository : IRepository<DelugeTorrent, Guid>
{
    Task<DelugeTorrent> GetByHashAsync(string hash);
    
    Task<List<DelugeTorrent>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string filter = null
    );
    
    Task<List<DelugeTorrent>> GetListPagedAsync(
        ISpecification<DelugeTorrent> spec,
        int skipCount,
        int maxResultCount,
        string sorting,
        bool includeDetails = false,
        CancellationToken cancellationToken = default
    );
}

