using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.SeriesNs;

public interface ISeriesRepository : IRepository<Series, Guid>
{
    Task<List<Series>> GetSeriesCollectionAsync(
        ISpecification<Series> spec,
        bool includeDetails = true,
        CancellationToken cancellationToken = default);
    
    Task<Series> FindBySeriesNameYear(string name, 
        int firstAiredYear,
        bool includeDetails = true);
    Task<List<Series>> GetActiveList();

    Task<List<Series>> GetListPagedAsync(
        ISpecification<Series> spec,
        int skipCount,
        int maxResultCount,
        string sorting,
        bool includeDetails = true,
        CancellationToken cancellationToken = default);

    Task<List<Series>> GetNoDefault();
    
    Task<List<Series>> GetBySeriesName(string seriesName,
        bool includeDetails = true,
        CancellationToken cancellationToken = default);
    
    Task<List<Series>> GetSeriesBySpec(
        ISpecification<Series> spec,
        bool includeDetails = true,
        CancellationToken cancellationToken = default);
    
    Task<List<Series>> GetDashboardAsync(
        ISpecification<Series> spec,
        bool includeDetails = true,
        CancellationToken cancellationToken = default);
    
}