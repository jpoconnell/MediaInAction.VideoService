using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using MediaInAction.EmbyService.EmbyShowsNs;
using MediaInAction.EmbyService.MongoDB;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;
using Volo.Abp.Specifications;

namespace MediaInAction.EmbyService.EmbyShowNs;

public class MongoDbEmbyShowRepository
    : MongoDbRepository<EmbyServiceMongoDbContext, EmbyShow, Guid>,
    IEmbyShowRepository
{
    public MongoDbEmbyShowRepository(
        IMongoDbContextProvider<EmbyServiceMongoDbContext> dbContextProvider
        ) : base(dbContextProvider)
    {
    }
    
    public async Task<EmbyShow> FindByNameAsync(string name)
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

    public async Task<List<EmbyShow>> GetListAllPagedAsync(
        int skipCount, 
        int maxResultCount, 
        string sorting,
        CancellationToken cancellationToken = default)
    {
        var queryable = await GetMongoQueryableAsync();
        return await queryable
            .OrderBy(sorting)
            .As<IMongoQueryable<EmbyShow>>()
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync();
    }

    public async Task<List<EmbyShow>> GetListPagedAsync(
        ISpecification<EmbyShow> spec,
        int skipCount,
        int maxResultCount,
        string sorting = "Name",
        bool includeDetails = true,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var queryable = await GetMongoQueryableAsync();
            
            return await queryable
                .OrderBy(sorting)
                .As<IMongoQueryable<EmbyShow>>()
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
            
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    public async Task<EmbyShow> FindByEmbyShowNameYearAsync(string name, int year)
    {
        try
        {
            var queryable = await GetMongoQueryableAsync();
            return await queryable.FirstOrDefaultAsync(show => show.Name == name && 
                                                               show.FirstAiredYear == year);
        }
        catch 
        {
            return null;
        }
    }

    public async Task<EmbyShow> FindBySlug(string showSlug, bool includeDetails = true, CancellationToken cancellationToken = default)
    {
        try
        {
            var queryable = await GetMongoQueryableAsync();
            return await queryable.FirstOrDefaultAsync(show => show.Slug == showSlug );
        }
        catch
        {
            return null;
        }
    }

    public async Task<List<EmbyShow>> GetDashboardAsync(ISpecification<EmbyShow> specification)
    {
        try
        {
            var queryable = await GetMongoQueryableAsync();
            
            return await queryable
                .As<IMongoQueryable<EmbyShow>>()
                .ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    public async Task<List<EmbyShow>> GetEmbyShowBySpec(ISpecification<EmbyShow> spec, 
        bool includeDetails = false)
    {
        try
        {
            var queryable = await GetMongoQueryableAsync();
            
            return await queryable
                .As<IMongoQueryable<EmbyShow>>()
                .ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    public Task<EmbyShow> FindSlugAsync(string slug)
    {
        throw new NotImplementedException();
    }

    public Task<EmbyShow> GetByNameAsync(string showName)
    {
        throw new NotImplementedException();
    }

    public Task<List<EmbyShow>> GetListAsync(ISpecification<EmbyShow> specification)
    {
        throw new NotImplementedException();
    }

    public Task<List<EmbyShow>> GetListPagedAsync(ISpecification<EmbyShow> specification, int inputSkipCount, int inputMaxResultCount, string inputSorting)
    {
        throw new NotImplementedException();
    }

    public Task<List<EmbyShow>> GetEmbyShowBySpec(ISpecification<EmbyShow> specification)
    {
        throw new NotImplementedException();
    }
}
