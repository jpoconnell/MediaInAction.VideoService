using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Specifications;

namespace MediaInAction.TraktService.TraktEpisodeNs;

public interface ITraktEpisodeRepository :  IRepository<TraktEpisode, Guid>
{
      Task<List<TraktEpisode>> GetDashboardAsync(
            ISpecification<TraktEpisode> spec);

      Task<TraktEpisode> FindByTraktShowSlugSeasonEpisodeAsync(
            string slug, 
            int season , int episode);
      
      Task<List<TraktEpisode>> GetListPagedAsync(
            string filter,
            int skipCount,
            int maxResultCount,
            string sorting,
            CancellationToken cancellationToken = default);

      Task<TraktEpisode> GetByIdentifier(string slug, 
            int season, 
            int episode);

      Task<List<TraktEpisode>> GetEpisodesByShow(string slug);
      Task<List<TraktEpisode>> GetChangedListAsync();
      Task<List<TraktEpisode>> GetTraktEpisodeBySpec(ISpecification<TraktEpisode> specification);
}