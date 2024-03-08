using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaInAction.VideoService.MovieAliasNs;
using MediaInAction.VideoService.MovieAliasNs.Dtos;
using MediaInAction.VideoService.MovieNs.Dtos;
using MediaInAction.VideoService.MovieNs.Specifications;
using MediaInAction.VideoService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.MovieNs;

[Authorize(VideoServicePermissions.Movies.Default)]
public class MovieAppService : VideoServiceAppService, IMovieAppService
{
    private readonly MovieManager _movieManager;
    private readonly IMovieRepository _movieRepository;
    private readonly ILogger<MovieAppService> _logger;
    
    public MovieAppService(MovieManager movieManager,
        IMovieRepository movieRepository,
        ILogger<MovieAppService> logger
    )
    {
        _movieManager = movieManager;
        _movieRepository = movieRepository;
        _logger = logger;
    }
    
    public async Task<MovieDto> GetAsync(Guid id)
    {
        var movie = await _movieRepository.GetAsync(id);
        return CreateMovieDtoMapping(movie);
    }
    
    [AllowAnonymous]
    public async Task<MovieDto> CreateAsync(MovieCreateDto input)
    {
        var newMovieAliases = MapDto(input.MovieAliases);

        var newMovie = await _movieManager.CreateMovieAsync
        (
            name: input.Name,
            year: input.FirstAiredYear,
            movieAliases: newMovieAliases
        );

        return CreateMovieDtoMapping(newMovie);
    }

    private List<MovieAlias> MapDto(List<MovieAliasCreateDto> inputMovieAliases)
    {
        var movieAliasList = new List<MovieAlias>();
        foreach (var movieAliasDto in inputMovieAliases)
        {
            var movieAlias = new MovieAlias();
            movieAlias.IdValue = movieAliasDto.IdValue;
            movieAlias.IdType = movieAliasDto.IdType;
            movieAlias.MovieId = movieAliasDto.MovieId;
            movieAliasList.Add(movieAlias);
        }

        return movieAliasList;
    }

    [AllowAnonymous]
    public async Task<List<MovieDto>> GetMoviesAsync(GetMoviesInput input)
    {
        ISpecification<Movie> specification = SpecificationFactory.Create(input.Filter);
        var movies = await _movieRepository.GetMoviesBySpec(specification, true);
        return CreateMovieDtoMapping(movies);
    }

    [AllowAnonymous]
    public async Task<MovieDto> GetByMovieNameAsync(string name)
    {
        var movie = await _movieRepository.GetByMovieNameAsync(name);
        Logger.LogInformation($" Movie recieved with movie no:{name}");
        return CreateMovieDtoMapping(movie);
    }
    

    [Authorize(VideoServicePermissions.Movies.SetAsInActive)]
    public async Task SetAsInActiveAsync(Guid id)
    {
        await _movieManager.SetInActiveAsync(id);
    }
    
    public async Task<PagedResultDto<MovieDto>> GetListPagedAsync(PagedAndSortedResultRequestDto input)
    {
        var movies = await _movieRepository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, input.Sorting ?? "MovieDate", true);

        var totalCount = await _movieRepository.GetCountAsync();
        return new PagedResultDto<MovieDto>(
            totalCount,
            CreateMovieDtoMapping(movies)
        );
    }
    
    public async Task<DashboardDto> GetDashboardAsync(DashboardInput input)
    {
        return new DashboardDto()
        {
            MovieStatusDto = await GetCountOfTotalMovieStatusAsync(input.Filter)
        };
    }

    public async Task<MovieDto> GetMovieAsync(string newMovieName, int newMovieFirstAiredYear)
    {
        var movie = await _movieRepository.GetByMovieNameAsync(newMovieName);

        var movieDto = new MovieDto();
        movieDto.Name = movie.Name;
        movieDto.FirstAiredYear = movie.FirstAiredYear;
        return movieDto;
    }

    private async Task<List<MovieStatusDto>> GetCountOfTotalMovieStatusAsync(string inputFilter)
    {
        ISpecification<Movie> specification = SpecificationFactory.Create(inputFilter);
        var movies = await _movieRepository.GetDashboardAsync(specification);
        return CreateMovieStatusDtoMapping(movies);
    }

    private List<( Guid movieId,string idType, string idValue)> 
       GetMovieAliasTuple(List<MovieAliasCreateDto> inMovieAliases)
    {
        var movieAliases =
            new List<( Guid movieId,string idType, string idValue)>();
        foreach (var inMovieAlias in inMovieAliases)
        {
            movieAliases.Add((Guid.NewGuid(), inMovieAlias.IdType, inMovieAlias.IdValue));
        }

        return movieAliases;
    }

    private List<MovieDto> CreateMovieDtoMapping(List<Movie> movies)
    {
        List<MovieDto> dtoList = new List<MovieDto>();
        foreach (var movie in movies)
        {
            dtoList.Add(CreateMovieDtoMapping(movie));
        }

        return dtoList;
    }

    private MovieDto CreateMovieDtoMapping(Movie movie)
    {
        return new MovieDto()
        {
            Name = movie.Name,
            FirstAiredYear = movie.FirstAiredYear,
            Id = movie.Id,
        };
    }

    private List<MovieStatusDto> CreateMovieStatusDtoMapping(List<Movie> movies)
    {
        var movieStatus = movies
                    .GroupBy(p => p.MovieStatus)
                    .Select(p => new MovieStatusDto { CountOfStatusMovie = p.Count(), MovieStatus = p.Key.ToString() })
                    .OrderBy(p => p.CountOfStatusMovie)
                    .ToList();

        return movieStatus;
    }
}
