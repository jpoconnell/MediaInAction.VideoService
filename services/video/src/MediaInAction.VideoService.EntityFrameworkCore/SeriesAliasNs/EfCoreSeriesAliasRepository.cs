using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaInAction.VideoService.EntityFrameworkCore;
using MediaInAction.VideoService.SeriesNs;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace MediaInAction.VideoService.SeriesAliasNs;

public class EfCoreSeriesAliasRepository : EfCoreRepository<VideoServiceDbContext, SeriesAlias, Guid>, ISeriesAliasRepository
{
    public EfCoreSeriesAliasRepository(IDbContextProvider<VideoServiceDbContext> dbContextProvider) : base(
        dbContextProvider)
    {
    }

    public async Task<List<SeriesAlias>> GetByIdValue(string requestSlug)
    {
        try
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .Where(e => e.IdValue == requestSlug )
                .ToListAsync();
        }
        catch 
        {
            return null;
        }
    }

    public Task<List<SeriesAlias>> GetByIdType(string idType)
    {
        throw new NotImplementedException();
    }

    public Task<SeriesAlias> FindBySeriesTypeValueAsync(Guid seriesId, string idType, string alias)
    {
        throw new NotImplementedException();
    }

    public Task<SeriesAlias> GetBySeriesType(Guid id, string type)
    {
        throw new NotImplementedException();
    }

    public Task<SeriesAlias> FindBySeriesTypeAsync(Guid seriesId, string idType)
    {
        throw new NotImplementedException();
    }

    public async Task<SeriesAlias> FindByIdValue(string idValue)
    {
        try
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .Where(e => e.IdValue == idValue )
                .FirstAsync();
        }
        catch 
        {
            return null;
        }
    }

    Task<SeriesAlias> ISeriesAliasRepository.GetByIdValue(string idValue)
    {
        throw new NotImplementedException();
    }
}