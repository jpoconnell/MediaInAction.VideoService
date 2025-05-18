using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using MediaInAction.TraktService.MongoDB;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;
using Volo.Abp.Specifications;

namespace MediaInAction.TraktService.TraktEpisodeNs;

public class MongoDbTraktEpisodeRepository
    : MongoDbRepository<TraktServiceMongoDbContext, TraktEpisode, Guid>,
    ITraktEpisodeRepository
{
    public MongoDbTraktEpisodeRepository(
        IMongoDbContextProvider<TraktServiceMongoDbContext> dbContextProvider
        ) : base(dbContextProvider)
    {
    }

    public async Task<List<TraktEpisode>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string filter = null)
    {
        var queryable = await GetMongoQueryableAsync();
        return await queryable
            .WhereIf<TraktEpisode, IMongoQueryable<TraktEpisode>>(
                !filter.IsNullOrWhiteSpace(),
                Episode => Episode.EpisodeName.Contains(filter)
            )
            .OrderBy(sorting)
            .As<IMongoQueryable<TraktEpisode>>()
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync();
    }

    public async Task<List<TraktEpisode>> GetAllListAsync()
    {
        var queryable = await GetMongoQueryableAsync();
        return await queryable
            .ToListAsync();
    }
    public async Task<List<TraktEpisode>> GetDashboardAsync(ISpecification<TraktEpisode> spec)
    {
        try
        {
            CancellationToken cancellationToken = default;
            var queryable = await GetMongoQueryableAsync();
            return await (await GetMongoQueryableAsync())
                .Where(spec.ToExpression())
                .ToListAsync(GetCancellationToken(cancellationToken));
        }
        catch
        {
            Console.WriteLine();
            return null;
        }
    }

    public async Task<TraktEpisode> FindByTraktShowSlugSeasonEpisodeAsync(
        string slug, int season, int episode)
    {
        try
        {
            var queryable = await GetMongoQueryableAsync();
            return await queryable.FirstOrDefaultAsync(Episode => Episode.SeriesId.ToString() == slug &&
                                                                  Episode.SeasonNum == season &&
                                                                  Episode.EpisodeNum == episode);
        }
        catch
        {
            return null;
        }
    }

    public async Task<List<TraktEpisode>> GetListPagedAsync(
        string filter,
        int skipCount, 
        int maxResultCount, 
        string sorting,
        CancellationToken cancellationToken = default)
    {
        var queryable = await GetMongoQueryableAsync();
        return await queryable
            .OrderBy(sorting)
            .As<IMongoQueryable<TraktEpisode>>()
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync();
    }

    public async Task<List<TraktEpisode>> GetListAllAsync()
    {
        var queryable = await GetMongoQueryableAsync();
        return await queryable
            .As<IMongoQueryable<TraktEpisode>>()
            .ToListAsync();
    }

    public async Task<TraktEpisode> GetByIdentifier(string slug, int season, int episode)
    {
        var queryable = await GetMongoQueryableAsync();
        return await queryable.FirstOrDefaultAsync(Episode => Episode.EpisodeName == slug &&
                                                              Episode.SeasonNum == season &&
                                                              Episode.EpisodeNum == episode);
        
    }

    public Task<List<TraktEpisode>> GetEpisodesBySlug(string slug)
    {
        throw new NotImplementedException();
    }

    public async Task<List<TraktEpisode>> GetEpisodesByShow(string showId)
    {
        var queryable = await GetMongoQueryableAsync();
        return await queryable
            .Where<TraktEpisode>(e => e.SeriesId == showId)
            .As<IMongoQueryable<TraktEpisode>>()
            .ToListAsync();
    }

    public async Task<List<TraktEpisode>> GetTraktEpisodeBySpec(ISpecification<TraktEpisode> spec)
    {
        try
        {
            CancellationToken cancellationToken = default;
            var queryable = await GetMongoQueryableAsync();
            return await (await GetMongoQueryableAsync())
                .Where(spec.ToExpression())
                .ToListAsync(GetCancellationToken(cancellationToken));
        }
        catch
        {
            Console.WriteLine();
            return null;
        }
    }

    public async Task<List<TraktEpisode>> GetBySlug(string slug)
    {
        try
        {
            CancellationToken cancellationToken = default;
            var queryable = await GetMongoQueryableAsync();
            return await (await GetMongoQueryableAsync())
                .Where(e => e.EpisodeName == slug)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }
        catch
        {
            return null;
        }
    }
}
