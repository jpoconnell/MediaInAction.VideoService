using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.Shared.Domain.Enums;
using MediaInAction.TraktService.TraktEpisodeAliasNs;
using Microsoft.Extensions.Logging;
using Volo.Abp.Domain.Services;

namespace MediaInAction.TraktService.TraktEpisodeNs
{
    public class TraktEpisodeManager : DomainService
    {
        private ILogger<TraktEpisodeManager> _logger;
        private readonly ITraktEpisodeRepository _traktEpisodeRepository;

        public TraktEpisodeManager(
            ITraktEpisodeRepository traktEpisodeRepository,
            ILogger<TraktEpisodeManager> logger)
        {
            _traktEpisodeRepository = traktEpisodeRepository;
            _logger = logger;
        }
        
        
        public async Task<TraktEpisode> CreateAsync(TraktEpisodeCreateDto traktEpisodeCreateDto)
        {
            try
            {
                if (traktEpisodeCreateDto.EpisodeName.IsNullOrEmpty())
                {
                    traktEpisodeCreateDto.EpisodeName = "Unknown";
                }
                
                // Create new traktEpisode
                var episode = new TraktEpisode
                {
                    ShowSlug = traktEpisodeCreateDto.ShowSlug,
                    SeasonNum = traktEpisodeCreateDto.SeasonNum,
                    EpisodeNum = traktEpisodeCreateDto.EpisodeNum,
                    EpisodeName = traktEpisodeCreateDto.EpisodeName,
                    AiredDate = traktEpisodeCreateDto.AiredDate,
                    Changed = true,
                    TraktEpisodeAliases = new List<TraktEpisodeAlias>()
                };

                foreach (var traktEpisodeAlias in traktEpisodeCreateDto.TraktEpisodeCreateAliases)
                {
                    var newTraktEpisodeAlias = new TraktEpisodeAlias();
                    newTraktEpisodeAlias.IdType = traktEpisodeAlias.IdType;
                    newTraktEpisodeAlias.IdValue = traktEpisodeAlias.IdValue;
                    episode.TraktEpisodeAliases.Add(newTraktEpisodeAlias);
                }
                
                var traktEpisode = await _traktEpisodeRepository.InsertAsync(episode);
                if (traktEpisode != null)
                {
                    _logger.LogInformation("Trakt Episode Created:" + episode.ShowSlug + ":" +
                                 episode.SeasonNum.ToString() + ":" + episode.EpisodeNum.ToString() + episode.EpisodeName);
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


        public async Task<TraktEpisode> UpdateTraktStatus(Guid traktId, FileStatus status)
        {
            try
            {
                var traktEpisode = await _traktEpisodeRepository.GetAsync(traktId);
                if (traktEpisode.TraktStatus != status)
                {
                    traktEpisode.TraktStatus = status;
                    await _traktEpisodeRepository.UpdateAsync(traktEpisode, true);
                    return traktEpisode;
                }
            }
            catch 
            {
                _logger.LogDebug("TraktEpisodeManager.UpdateTraktStatus: TraktEpisode Not Found " + traktId.ToString());
                return null;
            }
            return null;
        }
    }
}
