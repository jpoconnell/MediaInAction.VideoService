using System;
using System.Threading.Tasks;
using MediaInAction.Shared.Domain.Enums;
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
    
    public async Task<Movie> CreateAsync(MovieCreateDto movieCreateDto)
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
                movieType: MediaType.Movie,
                isActive: true
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
        catch (Exception ex)
        {

            return null;
        }

        return null;
    }
    
    public async Task<Movie> CreateUpdateMovieAsync(MovieCreateDto movieCreateDto)
    {
        if (movieCreateDto.Name != null)
        {
            if(movieCreateDto.FirstAiredYear < 1950)
            {
                movieCreateDto.FirstAiredYear = 1950;
            }
            var dbMovie = await _movieRepository.FindByMovieNameYear(movieCreateDto.Name, movieCreateDto.FirstAiredYear);
            if (dbMovie == null)
            {
                var createMovie = await CreateAsync(movieCreateDto);
                return createMovie;
            }
            else
            {
                var update = 0;
                if (dbMovie.IsActive != movieCreateDto.IsActive)
                {
                    dbMovie.IsActive = movieCreateDto.IsActive;
                    update++;
                }

                if (dbMovie.ImageName != movieCreateDto.ImageName)
                {
                    dbMovie.ImageName = movieCreateDto.ImageName;
                    update++;
                }
                if (dbMovie.IsActive != movieCreateDto.IsActive)
                {
                    dbMovie.IsActive = movieCreateDto.IsActive;
                    update++;
                }
       
                foreach (var movieAlias in movieCreateDto.MovieAliases)
                {
                    var found = false;
                    foreach (var dbMovieAlias in dbMovie.MovieAliases)
                    {
                        if ((dbMovieAlias.IdType == movieAlias.IdType) && (dbMovieAlias.IdValue == movieAlias.IdValue))
                        {
                            found = true;
                        }
                    }

                    if (found == false)
                    {
                        update++;
                    }
                }
            
                if (update > 0)
                {
                   var updatedMovie = await _movieRepository.UpdateAsync(dbMovie);
                   return updatedMovie;
                }
            }
        }
        return null;
    }
    
    public async Task SetInActiveAsync(Guid id)
    {
       var dbMovie = await _movieRepository.GetAsync(id);
       dbMovie.IsActive = false;
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

    public async Task<Movie> UpdateAsync(MovieCreateDto movieCreateDto)
    {
        var movieList = await _movieRepository.GetByMovieName(movieCreateDto.Name);
        var update = 0;
        if (movieList.Count == 1)
        {
            var movie = movieList[0];
            if (movieCreateDto.MovieAliases.Count != movie.MovieAliases.Count)
            {
                foreach (var movieAlias in movieCreateDto.MovieAliases)
                {
                    var found = false;
                    foreach (var dbMovieAlias in movie.MovieAliases)
                    {
                        if ((dbMovieAlias.IdType == movieAlias.IdType) && (dbMovieAlias.IdValue == movieAlias.IdValue))
                        {
                            found = true;
                        }
                    }

                    if (found == false)
                    {
                        movie.AddMovieAlias(
                            id: GuidGenerator.Create(),
                            movieId: movie.Id,
                            idType: movieAlias.IdType,
                            idValue: movieAlias.IdValue
                        );
                        update++;
                    }
                }
            }

            if (movie.IsActive != movieCreateDto.IsActive)
            {
                movie.IsActive = movieCreateDto.IsActive;
                update++;
            }

            if (movie.ImageName != movieCreateDto.ImageName)
            {
                movie.ImageName = movieCreateDto.ImageName;
                update++;
            }
            if (movie.MediaStatus != movieCreateDto.MediaStatus)
            {
                movie.MediaStatus = movieCreateDto.MediaStatus;
                update++;
            }   
            
            if (update > 0)
            {
                var updatedMovie = await _movieRepository.UpdateAsync(movie);
                return updatedMovie;
            }
            return null;
        }
        else
        {
            return await CreateAsync(movieCreateDto);
        }
    }
}
