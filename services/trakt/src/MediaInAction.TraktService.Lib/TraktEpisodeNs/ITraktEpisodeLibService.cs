using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.TraktService.Lib.TraktShowNs.Dtos;
using MediaInAction.TraktService.TraktEpisodeNs;

namespace MediaInAction.TraktService.Lib.TraktEpisodeNs;

public interface ITraktEpisodeLibService
{
    Task UpdateAddFromDto(CollectionEpisodeDto episode);
    Task CreateUpdateEpisode(CollectionEpisodeDto episode);
    Task<List<TraktEpisodeDto>> GetEpisodeByShow(string slug);
   
}
