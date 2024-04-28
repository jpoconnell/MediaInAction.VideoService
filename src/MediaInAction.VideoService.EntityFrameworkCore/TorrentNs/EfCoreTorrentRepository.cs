using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using MediaInAction.VideoService.EntityFrameworkCore;
using MediaInAction.VideoService.TorrentsNs;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

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
}