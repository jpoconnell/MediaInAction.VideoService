using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using MediaInAction.TraktService.MongoDB;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;
using Volo.Abp.Specifications;

namespace MediaInAction.TraktService.TraktMovieNs;

public class MongoDbTraktMovieRepository
    : MongoDbRepository<TraktServiceMongoDbContext, TraktMovie, Guid>,
    ITraktMovieRepository
{
    public MongoDbTraktMovieRepository(
        IMongoDbContextProvider<TraktServiceMongoDbContext> dbContextProvider
        ) : base(dbContextProvider)
    {
    }
    
    public async Task<TraktMovie> FindByNameAsync(string name)
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
    
    public async Task<TraktMovie> GetByTraktMovieNameYearAsync(string name, int year)
    {
        var queryable = await GetMongoQueryableAsync();
        return await queryable.FirstOrDefaultAsync(show => show.Name == name && show.FirstAiredYear == year);
    }

    public async Task<List<TraktMovie>> GetListAllPagedAsync(
        int skipCount, 
        int maxResultCount, 
        string sorting,
        CancellationToken cancellationToken = default)
    {
        var queryable = await GetMongoQueryableAsync();
        return await queryable
            .OrderBy(sorting)
            .As<IMongoQueryable<TraktMovie>>()
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync();
    }

    public async Task<List<TraktMovie>> GetDashboardAsync(ISpecification<TraktMovie> spec)
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
    public async Task<TraktMovie> FindBySlugAsync(string slug)
    {
        try
        {
            var queryable = await GetMongoQueryableAsync();
            return await queryable
                .FirstOrDefaultAsync(show => show.Slug == slug );
        }
        catch
        {
            return null;
        }
    }
    
    public async Task<List<TraktMovie>> GetDashboardAsync(ISpecification<TraktMovie> spec, 
        bool includeDetails = true, 
        CancellationToken cancellationToken = default)
    {
        try
        {
            var queryable = await GetMongoQueryableAsync();
            return await (await GetMongoQueryableAsync())
                .Where(spec.ToExpression())
                .ToListAsync(GetCancellationToken(cancellationToken));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }
    

    public async Task<List<TraktMovie>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string filter = null)
    {
        var queryable = await GetMongoQueryableAsync();
        return await queryable
            .WhereIf<TraktMovie, IMongoQueryable<TraktMovie>>(
                !filter.IsNullOrWhiteSpace(),
                movie => movie.Slug.Contains(filter)
            )
            .OrderBy(sorting)
            .As<IMongoQueryable<TraktMovie>>()
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync();
    }

    public async Task<TraktMovie> GetByMovieNameYearAsync(string name, int year)
    {
        var queryable = await GetMongoQueryableAsync();
        return await queryable.FirstOrDefaultAsync(show => show.Name == name  && show.FirstAiredYear == year);
    }

    public async Task<TraktMovie> GetBySlug(string showSlug)
    {
        try
        {
            var queryable = await GetMongoQueryableAsync();
            return await queryable.FirstOrDefaultAsync(show => show.Slug == showSlug);
        }
        catch
        {
            return null;
        }
    }
}
