using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Specifications;

namespace MediaInAction.EmbyService.EmbyEpisodeNs;

public interface IEmbyEpisodeRepository :  IRepository<EmbyEpisode, Guid>
{
    Task<EmbyEpisode> FindBySeriesSeasonEpisodeAsync(string seriesId, int season,int episode);
  
    Task<List<EmbyEpisode>> GetListPagedAsync(ISpecification<EmbyEpisode> specification, 
        int inputSkipCount, 
        int inputMaxResultCount, 
        string inputSorting);
    
    Task<List<EmbyEpisode>> GetListAsync(
        ISpecification<EmbyEpisode> spec,
        bool includeDetails = true,
        CancellationToken cancellationToken = default);
    
    Task<List<EmbyEpisode>> GetDashboardAsync(
        ISpecification<EmbyEpisode> spec,
        bool includeDetails = true,
        CancellationToken cancellationToken = default);
}