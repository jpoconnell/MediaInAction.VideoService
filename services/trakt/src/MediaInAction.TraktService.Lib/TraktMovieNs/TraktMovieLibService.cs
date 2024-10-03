using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.TraktService.Lib.TraktMovieNs.Dtos;
using MediaInAction.TraktService.TraktMovieNs;
using Microsoft.Extensions.Logging;

namespace MediaInAction.TraktService.Lib.TraktMovieNs
{
    public class TraktMovieLibService : ITraktMovieLibService
    {
        private readonly ILogger<TraktMovieLibService> _logger;
        private readonly TraktMovieManager _traktMovieManager;
        private readonly ITraktMovieRepository _traktMovieRepository;
        
        public TraktMovieLibService(
            TraktMovieManager traktMovieManager,
            ITraktMovieRepository traktMovieRepository,
            ILogger<TraktMovieLibService> logger)
        {
            _traktMovieManager = traktMovieManager;
            _traktMovieRepository = traktMovieRepository;
            _logger = logger;
        }

        public async Task UpdateAddFromDto(TraktCollectionMovieDto movie)
        {
            _logger.LogInformation("MovieLibService.UpdateAddFromDto:" + movie.Name);
            try 
            { 
                await CreateUpdateMovie(movie);
            }
            catch (Exception ex)
            {
                _logger.LogDebug("MovieLibService.UpdateAddFromDto:" + ex.Message);
            }
        }
        
        public async Task<List<TraktMovieDto>> GetListAsync()
        {
            var traktMovieListDto = new List<TraktMovieDto>();
            var traktMovieList = await _traktMovieRepository.GetListAsync();
            foreach (var traktMovie in traktMovieList)
            {
                var traktMovieDto = new TraktMovieDto();
                traktMovieDto.FirstAiredYear = traktMovie.FirstAiredYear;
                traktMovieDto.Name = traktMovie.Name;
                traktMovieListDto.Add(traktMovieDto);
            }

            return traktMovieListDto;
        }

        private async Task<Guid> CreateUpdateMovie(TraktCollectionMovieDto traktCollectionMovieDto)
        {
            try
            {
                var found = false;
                var traktCreateMovieDto = new TraktMovieCreateDto();
                if ((!traktCollectionMovieDto.Slug.IsNullOrEmpty()) || (!traktCollectionMovieDto.Name.IsNullOrEmpty()))
                {
                    if (!traktCollectionMovieDto.Slug.IsNullOrEmpty())
                    {
                        var dbMovie = await _traktMovieRepository.FindBySlugAsync(traktCollectionMovieDto.Slug);
                        if (dbMovie != null)
                        {
                            found = true;
                         //   traktMovieDto = TranslateToTraktMovieDto(dbMovie,traktCollectionMovieDto);
                        }
                        else
                        {
                            traktCreateMovieDto = TranslateToCreateDto(traktCollectionMovieDto);
                        }
                  
                    }
                    else if (!traktCollectionMovieDto.Name.IsNullOrEmpty())
                    {
                        var dbMovie = await _traktMovieRepository.GetByMovieNameYearAsync(
                            traktCollectionMovieDto.Name, traktCollectionMovieDto.FirstAiredYear);
                        
                        if (dbMovie != null)
                        {
                            found = true;
                            var traktMovie = await UpdateTrakMovie(dbMovie, traktCollectionMovieDto);
                            if (traktMovie != null)
                            {
                                return traktMovie.Id;
                            }
                            else
                            {
                                return Guid.Empty;
                            }
                        }
                        else
                        {
                            var traktCreateTraktMovie = TranslateToCreateDto(traktCollectionMovieDto);
                            var createdTraktMovieDto = await _traktMovieManager.CreateAsync(traktCreateTraktMovie);
                            if (createdTraktMovieDto != null)
                            {
                                //await _movieClient.CreateMovie(createdTraktMovieDto);
                                //return createdTraktMovieDto.Id;
                            }
                            traktCreateMovieDto = TranslateToCreateDto(traktCollectionMovieDto);
                        }
                    }
                }

                if (found == true)
                {

                    var dbUpdatedMovie = await _traktMovieManager.UpdateAsync(traktCreateMovieDto);
                    if (dbUpdatedMovie != null)
                    {
                        _logger.LogInformation("Trakt Movie Created:" + traktCollectionMovieDto.Name + 
                                               ":" + traktCollectionMovieDto.FirstAiredYear.ToString());
                    }
                    return dbUpdatedMovie.Id;
                }
                else
                {
                    var dbCreatedMovie = await _traktMovieManager.CreateAsync(traktCreateMovieDto);
                    return dbCreatedMovie.Id;
                }
            }
            catch (Exception ex)
            {
                _logger.LogDebug("CreateUpdateMovie" +ex.Message);
                return Guid.Empty;
            }
        }
        
        private TraktMovieCreateDto TranslateToCreateDto(TraktCollectionMovieDto traktCollectionMovieDto)
        {
            var traktMovieCreateDto = new TraktMovieCreateDto
            {
                Name = traktCollectionMovieDto.Name,
                FirstAiredYear = traktCollectionMovieDto.FirstAiredYear,
                IsActive = true,
                Slug = traktCollectionMovieDto.Slug,
                TraktMovieCreateAliases = new List<TraktMovieAliasCreateDto>()
            };

            if (traktCollectionMovieDto.Slug.IsNullOrEmpty())
            {
                var slug = "";
                foreach (var traktAlias in traktCollectionMovieDto.TraktCollectionMovieAliasDtos)
                {
                    if (traktAlias.IdType == "slug")
                    {
                        slug = traktAlias.IdValue;
                    }
                }

                traktMovieCreateDto.Slug = slug;
            }
            else
            {
                traktMovieCreateDto.Slug = traktCollectionMovieDto.Slug;
            }

            if (traktCollectionMovieDto.TraktCollectionMovieAliasDtos.Count > 0)
            {
                if (traktMovieCreateDto.TraktMovieCreateAliases == null)
                {
                    traktMovieCreateDto.TraktMovieCreateAliases = new List<TraktMovieAliasCreateDto>();
                }
                foreach (var traktCollectionMovieAlias in traktCollectionMovieDto.TraktCollectionMovieAliasDtos)
                {
                    var traktMovieCreateAliasDto = new TraktMovieAliasCreateDto();
                    traktMovieCreateAliasDto.IdType = traktCollectionMovieAlias.IdType;
                    traktMovieCreateAliasDto.IdValue = traktCollectionMovieAlias.IdValue;
                    traktMovieCreateDto.TraktMovieCreateAliases.Add(traktMovieCreateAliasDto);
                }
            }
            
            return traktMovieCreateDto;
        }

        private string GetSlugFromAlias(TraktCollectionMovieDto traktMovieDto)
        {
            var result = "";
            foreach (var alias in traktMovieDto.TraktCollectionMovieAliasDtos)
            {
                if (alias.IdType == "slug")
                {
                    result = alias.IdValue;
                    break;
                }
            }

            return result;
        }

        private async Task<TraktMovieDto> UpdateTrakMovie(TraktMovie dbMovie, 
            TraktCollectionMovieDto traktMovieDto)
        {
            // return Id only if updated
            var diff = CompareMovie(dbMovie, traktMovieDto);
            if (diff)
            {
               // var updateMovieDto = TranslateToTraktMovieDto(dbMovie,traktMovieDto);
               // var updatedMovie = await _traktMovieManager.UpdateAsync(updateMovieDto);
               // return updatedMovie;
               return null;
            }
            return null;
        }

        private bool CompareMovie(TraktMovie dbMovie, 
            TraktCollectionMovieDto traktMovieDto)
        {
            var diff = new bool();
            diff = false;
            if (dbMovie.TraktMovieAliases.Count != traktMovieDto.TraktCollectionMovieAliasDtos.Count)
            {
                diff = true;
            }

            var slug = "";
            foreach (var alias in traktMovieDto.TraktCollectionMovieAliasDtos)
            {
                if (alias.IdType == "slug")
                {
                    slug = alias.IdValue;
                }
            }
            traktMovieDto.Slug = slug;
            
            if (dbMovie.Slug != traktMovieDto.Slug)
            {
                diff = true;
            }
            
            if (dbMovie.Name != traktMovieDto.Name)
            {
                diff = true;
            }
            if (dbMovie.FirstAiredYear != traktMovieDto.FirstAiredYear)
            {
                diff = true;
            }
            
            foreach (var alias in traktMovieDto.TraktCollectionMovieAliasDtos)
            {
                var found = false;
                foreach (var dbAlias in dbMovie.TraktMovieAliases)
                {
                    if (dbAlias.IdType == alias.IdType)
                    {
                        found = true;
                    }
                }
                if (found == false)
                {
                    diff = true;
                }
            }
            return diff;
        }
    }
}
