using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediaInAction.TraktService.TraktEpisodeNs.Dtos;

namespace MediaInAction.TraktService.TraktEpisodeNs;

public interface ITraktEpisodePublicService
{
    [ItemNotNull]
    Task<TraktEpisodeDto> GetAsync(Guid episodeId);

    Task<TraktEpisodeDto> FindByTraktShowSlugSeasonEpisodeAsync(string slug, int seasonNum, int episodeNum);
    Task<List<TraktEpisodeDto>> GetListAsync();
    Task<List<TraktEpisodeDto>> GetEpisodesByShow(string slug);
    Task<TraktEpisodeDto> UpdateAsync(TraktEpisodeDto updatedShow );
    Task<TraktEpisodeDto> CreateAsync(TraktEpisodeCreateDto traktEpisodeCreateDto);

}