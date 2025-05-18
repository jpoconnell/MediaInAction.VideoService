using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core;
using MediaInAction.VideoService.MovieNs;
using Microsoft.Extensions.Logging;
using Moviegrpc;
using VideoService.Movie.GrpcServer;

namespace MediaInAction.VideoService.Grpc;

public class PublicMovieGrpService : MovieGrpcService.MovieGrpcServiceBase
{
    private readonly ILogger<PublicMovieGrpService> _logger;
    private readonly IMovieRepository _movieRepository;
    private readonly MovieManager _movieManager;
    
    public PublicMovieGrpService(ILogger<PublicMovieGrpService> logger, 
        IMovieRepository movieRepository,
        MovieManager movieManager)
    {
        _logger = logger;
        _movieRepository = movieRepository;
        _movieManager = movieManager;
    }

    public override async Task<MovieModel> CreateUpdateMovie(MovieModel request, ServerCallContext context)
    {
        var movieCreateDto = TranslateMovieGrpc(request);
        var response = await _movieManager.CreateUpdateAsync(movieCreateDto);

        var movieModel = TranslateMovie(response);
        return movieModel;
    }
    
    public override async Task SearchMovies(SearchRequest request, IServerStreamWriter<MovieModel> responseStream, ServerCallContext context)
    {
        var movieList = await  _movieRepository.GetListAsync();
        foreach (var movie in movieList)
        {
            bool match = false;
            if (request.Slug.Length > 0)
            {
                foreach (var MovieAlias in movie.MovieAliases)
                {
                    if ((MovieAlias.IdType == "slug") && (MovieAlias.IdValue.ToUpper().Contains(request.Slug.ToUpper())))
                    {
                        match = true;
                    }
                }
            }
            if (request.Name.Length > 0)
            {
                if (movie.Name.ToUpper().Contains(request.Name.ToUpper()))
                {
                    match = true;
                }
            }
            if (match)
            {
                await Task.Delay(1000);
                var movieModel = TranslateMovie(movie);
                await responseStream.WriteAsync(movieModel);
            }
        }
    }
    
    private MovieModel TranslateMovie(Movie movie)
    {
        var movieModel = new MovieModel();
        movieModel.Name = movie.Name;
        movieModel.Year = movie.FirstAiredYear;
        
        foreach (var movieAlias in movie.MovieAliases)
        {
            var newMovieAlias = new MovieAliasModel();
            newMovieAlias.IdType = movieAlias.IdType;
            newMovieAlias.IdValue = movieAlias.IdValue;
            movieModel.MovieAliases.Add(newMovieAlias);
            if (movieAlias.IdType == "Slug")
            {
                movieModel.Slug = movieAlias.IdValue;
            }
        }
        return movieModel;
    }

    private MovieCreateDto TranslateMovieGrpc(MovieModel request)
    {
        var movieCreateDto = new MovieCreateDto();
        movieCreateDto.Name = request.Name;
        movieCreateDto.FirstAiredYear = request.Year;
        movieCreateDto.MovieAliases = new List<MovieAliasCreateDto>();
        foreach (var movieAlias in request.MovieAliases)
        {
            movieCreateDto.MovieAliases.Add(new MovieAliasCreateDto
            {
                IdType = movieAlias.IdType,
                IdValue = movieAlias.IdValue,
            });
        }
        return movieCreateDto;
    }
}