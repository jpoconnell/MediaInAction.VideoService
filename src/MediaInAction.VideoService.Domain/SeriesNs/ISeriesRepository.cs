using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.SeriesNs;

public interface ISeriesRepository : IRepository<Series, Guid>
{
    Task<List<Series>> GetSeriessByUserId(
        Guid userId,
        ISpecification<Series> spec,
        bool includeDetails = true,
        CancellationToken cancellationToken = default);

    Task<List<Series>> GetSeriessAsync(
        ISpecification<Series> spec,
        bool includeDetails = true,
        CancellationToken cancellationToken = default);

    Task<List<Series>> GetDashboardAsync(
        ISpecification<Series> spec,
        bool includeDetails = true,
        CancellationToken cancellationToken = default);

    Task<Series> FindBySeriesNameYear(string seriesName, int seriesYear,   
        bool includeDetails = true,
        CancellationToken cancellationToken = default);

    Task<Series> GetByIdValue(string requestSlug);
    Task<Series> GetByIdAsync(Guid id);
}