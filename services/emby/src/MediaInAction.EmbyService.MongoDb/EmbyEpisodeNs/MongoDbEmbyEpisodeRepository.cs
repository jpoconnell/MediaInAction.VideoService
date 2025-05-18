using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using MediaInAction.EmbyService.MongoDB;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;
using Volo.Abp.Specifications;

namespace MediaInAction.EmbyService.EmbyEpisodeNs;

public class MongoDbEmbyEpisodeRepository
    : MongoDbRepository<EmbyServiceMongoDbContext, EmbyEpisode, Guid>,
    IEmbyEpisodeRepository
{
    public MongoDbEmbyEpisodeRepository(
        IMongoDbContextProvider<EmbyServiceMongoDbContext> dbContextProvider
        ) : base(dbContextProvider)
    {
    }
    
    public async Task<List<EmbyEpisode>> GetListAllPagedAsync(
        int skipCount, 
        int maxResultCount, 
        string sorting,
        CancellationToken cancellationToken = default)
    {
        var queryable = await GetMongoQueryableAsync();
        return await queryable
            .OrderBy(sorting)
            .As<IMongoQueryable<EmbyEpisode>>()
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync();
    }

    public Task<EmbyEpisode> FindBySeriesSeasonEpisodeAsync(
        string seriesId, 
        int season, 
        int episode)
    {
        throw new NotImplementedException();
    }

    public Task<List<EmbyEpisode>> GetListPagedAsync(
        ISpecification<EmbyEpisode> specification, 
        int inputSkipCount, 
        int inputMaxResultCount, 
        string inputSorting)
    {
        throw new NotImplementedException();
    }

    public Task<List<EmbyEpisode>> GetListAsync(
        ISpecification<EmbyEpisode> spec, 
        bool includeDetails = true, 
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<List<EmbyEpisode>> GetListAsync()
    {
        var queryable = await GetMongoQueryableAsync();
        return await queryable
            .As<IMongoQueryable<EmbyEpisode>>()
            .ToListAsync();
    }

    public Task<List<EmbyEpisode>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null)
    {
        throw new NotImplementedException();
    }

    public Task<List<EmbyEpisode>> GetDashboardAsync(
        ISpecification<EmbyEpisode> spec, 
        bool includeDetails = true, 
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<List<EmbyEpisode>> GetListAllAsync()
    {
        var queryable = await GetMongoQueryableAsync();
        return await queryable
            .As<IMongoQueryable<EmbyEpisode>>()
            .ToListAsync();
    }
}
