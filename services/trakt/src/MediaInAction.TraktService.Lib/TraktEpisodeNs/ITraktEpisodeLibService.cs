using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.TraktService.Lib.TraktShowNs.Dtos;
using MediaInAction.TraktService.TraktEpisodeNs;
using MediaInAction.TraktService.TraktMovieNs;

namespace MediaInAction.TraktService.Lib.TraktEpisodeNs;

public interface ITraktEpisodeLibService
{
    Task UpdateAddFromDto(TraktEpisodeCreateDto episode);
}
