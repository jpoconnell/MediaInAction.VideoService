using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.TraktService.TraktMovieAliasNs;
using Microsoft.Extensions.Logging;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Distributed;

namespace MediaInAction.TraktService.TraktMovieNs
{
    public class TraktMovieManager : DomainService
    {
        private readonly ITraktMovieRepository _movieRepository;
        private readonly IDistributedEventBus _distributedEventBus;
        private ILogger<TraktMovieManager> _logger;
        
        public TraktMovieManager(
            ITraktMovieRepository traktMovieRepository,
            IDistributedEventBus distributedEventBus,
            ILogger<TraktMovieManager> logger
        )
        {
            _movieRepository = traktMovieRepository;
            _distributedEventBus = distributedEventBus;
            _logger = logger;
        }

        public async Task<TraktMovie> CreateAsync(TraktMovieCreateDto traktMovieCreateDto)
        {
            var existingMovie = new TraktMovie();
            if (traktMovieCreateDto.Slug != null)
            {
                 existingMovie = await _movieRepository.FindBySlugAsync(traktMovieCreateDto.Slug);
            }
            else if (traktMovieCreateDto.Name != null)
            {
                existingMovie = await _movieRepository.FindByNameAsync(traktMovieCreateDto.Name);
            }
            else
            {
                return null;
            }
            if (existingMovie != null)
            {
                return null;
            }
            else
            {
                var traktMovieId = GuidGenerator.Create();
                var newTraktMovieAliases = new List<TraktMovieAlias>();
                foreach (var traktMovieCreateAlias in traktMovieCreateDto.TraktMovieCreateAliases)
                {
                    var newTraktMovieAlias = new TraktMovieAlias();
                    //newTraktMovieAlias.MovieId = traktMovieId;
                    newTraktMovieAlias.IdType = traktMovieCreateAlias.IdType;
                    newTraktMovieAlias.IdValue = traktMovieCreateAlias.IdValue;
                    newTraktMovieAliases.Add(newTraktMovieAlias);
                }
                
                var newTraktMovie = new TraktMovie(
                    traktMovieId,
                    traktMovieCreateDto.Name,
                    traktMovieCreateDto.FirstAiredYear,
                    newTraktMovieAliases,
                    traktMovieCreateDto.Slug
                );
                var createdMovie = await _movieRepository.InsertAsync(newTraktMovie, true);
                // TODO: send create event
                return createdMovie;
            }
        }

        public async Task<TraktMovieDto> UpdateAsync(TraktMovieCreateDto traktMovieDto)
        {
            var changed = false;
            var existingMovie = await _movieRepository.FirstOrDefaultAsync(p => p.Slug == traktMovieDto.Slug);
            
            if (existingMovie.TraktMovieAliases.Count != traktMovieDto.TraktMovieCreateAliases.Count)
            {
                changed = true;
            }
            if (existingMovie.FirstAiredYear != traktMovieDto.FirstAiredYear)
            {
                existingMovie.FirstAiredYear = traktMovieDto.FirstAiredYear;
                changed = true;
            }
            if (existingMovie.Name != traktMovieDto.Name)
            {
                existingMovie.Name = traktMovieDto.Name;
                changed = true;
                
            }
            
            if (changed == true)
            {
                var updateMovie = new TraktMovieDto();
            
                // TODO: send update event
          
                var traktMovie = await _movieRepository.UpdateAsync(existingMovie, true);
                var updatedTraktMovie = MapToDto(traktMovie);
                return updatedTraktMovie;
            }
            else
            {
                return null;
            }
        }

        private TraktMovieDto MapToDto(TraktMovie updatedTraktMovie)
        {
            var traktMovieAliasDtos = new List<TraktMovieAliasDto>();

            foreach (var traktMovieAlias in updatedTraktMovie.TraktMovieAliases)
            {
                var traktMovieAliasDto = new TraktMovieAliasDto();
                traktMovieAliasDto.IdType = traktMovieAlias.IdType;
                traktMovieAliasDto.IdValue = traktMovieAlias.IdValue;
                traktMovieAliasDtos.Add(traktMovieAliasDto);
            }
            
            return new TraktMovieDto
            {
                Id = updatedTraktMovie.Id,
                Name = updatedTraktMovie.Name,
                FirstAiredYear = updatedTraktMovie.FirstAiredYear,
                TraktMovieAliasDtos = traktMovieAliasDtos               
            };
        }
    }
}
