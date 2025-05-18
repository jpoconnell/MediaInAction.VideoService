using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using MediaInAction.DelugeService.DelugeTorrentsNs;
using MediaInAction.DelugeService.MongoDB;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;
using Volo.Abp.Specifications;

namespace MediaInAction.DelugeService.TorrentNs;

public class MongoDbDelugeTorrentRepository
    : MongoDbRepository<DelugeServiceMongoDbContext, DelugeTorrent, Guid>,
    IDelugeTorrentRepository
{
    public MongoDbDelugeTorrentRepository(
        IMongoDbContextProvider<DelugeServiceMongoDbContext> dbContextProvider
        ) : base(dbContextProvider)
    {
    }
    
    public async Task<DelugeTorrent> FindByNameAsync(string name)
    {
        try
        {
            var queryable = await GetMongoQueryableAsync();
            return await queryable.FirstOrDefaultAsync(show => show.Name == name);
        }
        catch 
        {
            return null;
        }
    }

    public async Task<List<DelugeTorrent>> GetListAllPagedAsync(
        int skipCount, 
        int maxResultCount, 
        string sorting,
        CancellationToken cancellationToken = default)
    {
        var queryable = await GetMongoQueryableAsync();
        return await queryable
            .OrderBy(sorting)
            .As<IMongoQueryable<DelugeTorrent>>()
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync();
    }


    public Task<DelugeTorrent> GetByHashAsync(string hash)
    {
        throw new NotImplementedException();
    }

    public Task<List<DelugeTorrent>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null)
    {
        throw new NotImplementedException();
    }

    public Task<List<DelugeTorrent>> GetListPagedAsync(ISpecification<DelugeTorrent> spec, int skipCount, int maxResultCount, string sorting,
        bool includeDetails = false, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<DelugeTorrent>> GetDashboardAsync(ISpecification<DelugeTorrent> specification)
    {
        throw new NotImplementedException();
    }
}
