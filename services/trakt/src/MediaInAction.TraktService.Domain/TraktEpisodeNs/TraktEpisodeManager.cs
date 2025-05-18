using System;
using System.Threading.Tasks;
using MediaInAction.TraktService.TraktShowNs;
using Microsoft.Extensions.Logging;
using Volo.Abp.Domain.Services;

namespace MediaInAction.TraktService.TraktEpisodeNs
{
    public class TraktEpisodeManager : DomainService
    {
        private ILogger<TraktEpisodeManager> _logger;
        private readonly ITraktEpisodeRepository _traktEpisodeRepository;
        private readonly ITraktShowRepository _traktShowRepository;
        
        public TraktEpisodeManager(
            ITraktEpisodeRepository traktEpisodeRepository,
            ITraktShowRepository traktShowRepository,
            ILogger<TraktEpisodeManager> logger)
        {
            _traktEpisodeRepository = traktEpisodeRepository;
            _traktShowRepository = traktShowRepository;
            _logger = logger;
        }
        
        public async Task<TraktEpisode> CreateAsync(TraktEpisodeCreateDto traktEpisodeCreateDto)
        {
            try
            {
                var traktSeries = await _traktShowRepository.FindBySlugAsync(traktEpisodeCreateDto.Slug);
                
                // Create new order
                TraktEpisode episode = new TraktEpisode(
                    id: GuidGenerator.Create(),
                    seriesId: traktSeries.Id.ToString(),
                    seriesSlug: traktSeries.Slug,
                    seasonNum: traktEpisodeCreateDto.SeasonNum,
                    episodeNum: traktEpisodeCreateDto.EpisodeNum,
                    airedDate: traktEpisodeCreateDto.AiredDate,
                    name: traktEpisodeCreateDto.EpisodeName,
                    status: traktEpisodeCreateDto.Status
                    );

                // Add new order items
                foreach (var episodeAlias in traktEpisodeCreateDto.TraktEpisodeCreateAliases)
                {
                    episode.AddTraktEpisodeAlias(
                        id: GuidGenerator.Create(),
                        idType: episodeAlias.IdType,
                        idValue: episodeAlias.IdValue
                    );
                }
                
                var traktEpisode = await _traktEpisodeRepository.InsertAsync(episode,true);
                if (traktEpisode != null)
                {
                    _logger.LogInformation("Trakt Episode Created:" + traktEpisodeCreateDto.Slug + ":" +
                                 episode.SeasonNum.ToString() + ":" + episode.EpisodeNum.ToString() + " "+ episode.EpisodeName);
                    return traktEpisode;
                }
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex.Message);
                return null;
            }

            return null;
        }

    }
}
