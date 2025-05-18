using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Distributed;

namespace MediaInAction.TraktService.TraktMovieNs
{
    public class TraktMovieManager : DomainService
    {
        private readonly ITraktMovieRepository _movieRepository;
        private ILogger<TraktMovieManager> _logger;
        
        public TraktMovieManager(
            ITraktMovieRepository traktMovieRepository,
            IDistributedEventBus distributedEventBus,
            ILogger<TraktMovieManager> logger
        )
        {
            _movieRepository = traktMovieRepository;
            _logger = logger;
        }

        public async Task<TraktMovie> CreateAsync(TraktMovieCreateDto traktMovieCreateDto)
        {
            try 
            {
                // Create new order
                TraktMovie movie = new TraktMovie(
                    id: GuidGenerator.Create(),
                    name: traktMovieCreateDto.Name,
                    year: traktMovieCreateDto.FirstAiredYear,
                    slug: traktMovieCreateDto.Slug,
                    status: traktMovieCreateDto.Status
                );

                // Add new order items
                foreach (var movieAlias in traktMovieCreateDto.TraktMovieCreateAliases)
                {
                    movie.AddTraktMovieAlias(
                        id: GuidGenerator.Create(),
                        idType: movieAlias.IdType,
                        idValue: movieAlias.IdValue
                    );
                }

                var createdMovie = await _movieRepository.InsertAsync(movie, true);
                // TODO: send create event
                return createdMovie;
            } catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }
        
        
        public async Task ChangeNameAsync(
            [NotNull] TraktMovie movie,
            [NotNull] string newName)
        {
            Check.NotNull(movie, nameof(movie));
            Check.NotNullOrWhiteSpace(newName, nameof(newName));

            var existingAuthor = await _movieRepository.FindByNameAsync(newName);
            if (existingAuthor != null && existingAuthor.Id != movie.Id)
            {
                throw new TraktMovieNameAlreadyExistsException(newName);
            }

            movie.ChangeName(newName);
        }


        public async Task<TraktMovie> UpdateAsync(TraktMovieCreateDto traktMovieDto)
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
                var traktMovie = await _movieRepository.UpdateAsync(existingMovie, true);
                return traktMovie;
            }
            else
            {
                return null;
            }
        }
    }
}
