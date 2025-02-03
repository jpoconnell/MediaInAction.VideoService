using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediaInAction.VideoService.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.SeriesNs;

public class EfCoreSeriesRepository : EfCoreRepository<VideoServiceDbContext, Series, Guid>, ISeriesRepository
{
    public EfCoreSeriesRepository(IDbContextProvider<VideoServiceDbContext> dbContextProvider) : base(
        dbContextProvider)
    {
    }


    public Task<List<Series>> GetSeriessByUserId(Guid userId, ISpecification<Series> spec, bool includeDetails = true,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Series>> GetSeriessAsync(
        ISpecification<Series> spec,
        bool includeDetails = false,
        CancellationToken cancellationToken = default)
    {
        return await SeriesEfCoreQueryableExtensions.IncludeDetails((await GetDbSetAsync()), includeDetails)
            .Where(spec.ToExpression())
            .ToListAsync(GetCancellationToken(cancellationToken));
    }
    
    public async Task<List<Series>> GetDashboardAsync(
        ISpecification<Series> spec,
        bool includeDetails = true,
        CancellationToken cancellationToken = default)
    {
        return await (await GetDbSetAsync())
            .IncludeDetails(includeDetails)
            .Where(spec.ToExpression())
            .ToListAsync(GetCancellationToken(cancellationToken));
    }
    
    public async Task<Series> FindBySeriesNameYear(string seriesName, int seriesYear, 
        bool includeDetails = true,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet
                .IncludeDetails(includeDetails)
                .Where(e => e.Name == seriesName && e.FirstAiredYear == seriesYear )
                .FirstAsync();
        }
        catch 
        {
            return null;
        }
    }

    public Task<Series> GetByIdValue(string requestSlug)
    {
        throw new NotImplementedException();
    }

    public Task<Series> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }


    public override async Task<IQueryable<Series>> WithDetailsAsync()
    {
        return SeriesEfCoreQueryableExtensions.IncludeDetails((await GetQueryableAsync()));
    }
}