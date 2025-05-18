using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Specifications;

namespace MediaInAction.TraktService.TraktMovieNs;

public interface ITraktMovieRepository : IRepository<TraktMovie, Guid>
{
    Task<List<TraktMovie>> GetDashboardAsync(
        ISpecification<TraktMovie> spec,
        bool includeDetails = true,
        CancellationToken cancellationToken = default);
    
    Task<List<TraktMovie>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string filter = null
    );
    Task<TraktMovie> GetByMovieNameYearAsync(string name,int year);
    
    Task<TraktMovie> FindBySlugAsync(string slug);
    Task<TraktMovie> FindByNameAsync(string name);
}