using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;
using MediaInAction.VideoService.EntityFrameworkCore;
using MediaInAction.VideoService.Enums;
using MediaInAction.VideoService.TorrentsNs;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.TorrentNs;

public class EfCoreTorrentRepository : EfCoreRepository<VideoServiceDbContext, Torrent, Guid>, ITorrentRepository
{
    public EfCoreTorrentRepository(IDbContextProvider<VideoServiceDbContext> dbContextProvider) : base(
        dbContextProvider)
    {
    }
    
    public async Task<List<Torrent>> GetAllListAsync()
    {
        try
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .ToListAsync();
        }
        catch 
        {
            return null;
        }
    }

    public async Task<Torrent> FindByHash(string hash)
    {
        try
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .Where(e => e.Hash == hash )
                .FirstAsync();
        }
        catch 
        {
            return null;
        }
    }

    public async Task<List<Torrent>> GetUnMapped()
    {
        try
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .Where(e => e.IsMapped == false )
                .ToListAsync();
        }
        catch 
        {
            return null;
        }
    }

    public Task<List<Torrent>> GetTorrentStatus(FileStatus status)
    {
        throw new NotImplementedException();
    }

    public Task<List<Torrent>> GetMapped(bool mapped)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Torrent>> GetListPagedAsync(ISpecification<Torrent> spec, 
        int inputSkipCount, 
        int inputMaxResultCount, 
        string empty,
        CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync())
            .Where(spec.ToExpression())
            .ToListAsync(GetCancellationToken(cancellationToken));
    }
}