using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediaInAction.TraktService.TraktMovieNs.Dtos;

namespace MediaInAction.TraktService.TraktMovieNs;

public interface ITraktMoviePublicService
{
    [ItemNotNull]
    Task<TraktMovieDto> GetAsync(Guid movieId);
    Task<TraktMovieDto> GetByNameYear(TraktMovieCreateDto traktMovieCreateDto);
    Task<TraktMovieDto> CreateAsync(TraktMovieCreateDto traktMovieCreateDto);
    Task<List<TraktMovieDto>> GetListAsync();
    Task<TraktMovieDto> GetUniqueMovie(TraktMovieCreateDto traktMovieCreateDto);

    Task<TraktMovieDto> UpdateAsync(Guid id, UpdateTraktMovieDto traktMovieCreateDto2);
}