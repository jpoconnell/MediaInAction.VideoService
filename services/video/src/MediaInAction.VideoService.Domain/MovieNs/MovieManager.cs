using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Domain.Services;

namespace MediaInAction.VideoService.MovieNs;

public class MovieManager : DomainService
{
    private readonly ILogger<MovieManager> _logger;
    private readonly IMovieRepository _movieRepository;
    
    public MovieManager(IMovieRepository movieRepository,        
        ILogger<MovieManager> logger )
    {
        _movieRepository = movieRepository;
        _logger = logger;
    }
    
    public async Task<Movie> CreateUpdateAsync(MovieCreateDto movieCreateDto)
    {
        try
        {
            if (movieCreateDto.FirstAiredYear < 2000)
            {
                movieCreateDto.FirstAiredYear  = 2000;
            }
        
            var movieId = GuidGenerator.Create();
            // Create new movie
            var movie = new Movie(
                id: movieId,
                name: movieCreateDto.Name,
                firstAiredYear: movieCreateDto.FirstAiredYear,
                movieStatus: MovieStatus.Active
            );
            
            if (movieCreateDto.MovieAliases == null)
            {
                movie.AddMovieAlias(
                    id: GuidGenerator.Create(),
                    movieId: movieId,
                    idType: "name",
                    idValue: movieCreateDto.Name
                );
            }
            else
            {
                foreach (var movieAlias in movieCreateDto.MovieAliases)
                {
                    movie.AddMovieAlias(
                        id: GuidGenerator.Create(),
                        movieId: movie.Id,
                        idType: movieAlias.IdType,
                        idValue: movieAlias.IdValue);
                }

                var nameFound = false;
                var folderFound = false;
                foreach (var movieAlias in movieCreateDto.MovieAliases)
                {
                    if (movieAlias.IdType == "name")
                    {
                        nameFound = true;
                    }

                    if (movieAlias.IdType == "folder")
                    {
                        folderFound = true;
                    }
                }

                if (nameFound == false)
                {
                    movie.AddMovieAlias(
                        id: GuidGenerator.Create(),
                        movieId: movieId,
                        idType: "name",
                        idValue: movieCreateDto.Name
                    );
                }

                if (folderFound == false)
                {
                    movie.AddMovieAlias(
                        id: GuidGenerator.Create(),
                        movieId: movieId,
                        idType: "folder",
                        idValue: movieCreateDto.Name
                    );
                }
            }

            var createMovie = await _movieRepository.InsertAsync(movie, true);
            _logger.LogInformation("Movie created: {0}", createMovie.Name);
            return createMovie;
        }
        catch 
        {
            return null;
        }
    }
    
    public async Task SetInActiveAsync(Guid id)
    {
       var dbMovie = await _movieRepository.GetAsync(id);
       dbMovie.MovieStatus = MovieStatus.InActive;
       await _movieRepository.UpdateAsync(dbMovie, autoSave: true);
    }

    public async Task AddMovieAliasAsync(Guid id, MovieAliasCreateDto movieAliasCreateDto)
    {
        var dbMovie = await _movieRepository.GetAsync(id);
        bool found = false;
        foreach (var movieAlias in dbMovie.MovieAliases)
        {
            if (movieAliasCreateDto.IdType == movieAlias.IdType && movieAliasCreateDto.IdValue == movieAlias.IdValue)
            {
                found = true;
            }
        }
        if (found == false)
        {
            dbMovie.AddMovieAlias(
                id: GuidGenerator.Create(),
                movieId: id,
                idType: movieAliasCreateDto.IdType,
                idValue: movieAliasCreateDto.IdValue
            );
            await _movieRepository.UpdateAsync(dbMovie, autoSave: true);
        }
    }
    
}
