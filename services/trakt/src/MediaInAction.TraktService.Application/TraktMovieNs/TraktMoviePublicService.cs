using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.TraktService.TraktMovieNs.Dtos;
using Microsoft.Extensions.Logging;
using Moviegrpc;
using Volo.Abp;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.ObjectMapping;

namespace MediaInAction.TraktService.TraktMovieNs;

public class TraktMoviePublicService : ITraktMoviePublicService, ITransientDependency
{
    private readonly IDistributedCache<TraktMovieDto, Guid> _cache;
    private readonly ILogger<TraktMoviePublicService> _logger;
    private readonly IObjectMapper _mapper;
    private readonly MovieGrpcService.MovieGrpcServiceClient _movieGrpcServiceClient;
    private readonly ITraktMovieRepository _traktMovieRepository;
    private readonly TraktMovieManager _traktMovieManager;
    
    public TraktMoviePublicService(
        IDistributedCache<TraktMovieDto, Guid> cache,
        ILogger<TraktMoviePublicService> logger,
        ITraktMovieRepository  traktMovieRepository,
        TraktMovieManager traktMovieManager,
        IObjectMapper mapper)
    {
        _cache = cache;
        _logger = logger;
        _mapper = mapper;
        _traktMovieRepository = traktMovieRepository;
        _traktMovieManager = traktMovieManager;
        _movieGrpcServiceClient = new MovieGrpcService.MovieGrpcServiceClient(Grpc.Net.Client.GrpcChannel.ForAddress("https://localhost:8181"));
    }

    public async Task<TraktMovieDto> GetAsync(Guid movieId)
    {
        return (await _cache.GetOrAddAsync(
            movieId,
            () => GetOneMovieAsync(movieId)
        ))!;
    }

    public async Task<TraktMovieDto> GetByNameYear(TraktMovieCreateDto traktMovieCreateDto)
    {
        var traktMovie = await _traktMovieRepository.FindByNameAsync(traktMovieCreateDto.Name);
        return MapToDto(traktMovie);
    }

    private TraktMovieDto MapToDto(TraktMovie traktMovie)
    {
        return _mapper.Map<TraktMovie, TraktMovieDto>(traktMovie);
    }

    public async Task<TraktMovieDto> CreateAsync(TraktMovieCreateDto traktMovieCreateDto)
    {
        var rtn = await _traktMovieManager.CreateAsync(traktMovieCreateDto);
        if (rtn != null)
        {
            return MapToDto(rtn);
        }
        else
        {
            return null;
        }
      
    }

    public async Task<List<TraktMovieDto>> GetListAsync()
    {
        var traktMovieDtos = new List<TraktMovieDto>();
        var traktMovieList = await _traktMovieRepository.GetListAsync();
        return MapToDtos(traktMovieList);
    }

    public async Task<TraktMovieDto> GetUniqueMovie(TraktMovieCreateDto traktMovieCreateDto)
    {
        var dbMovie = await _traktMovieRepository.FindByNameAsync(traktMovieCreateDto.Name);
        if (dbMovie != null)
        {
            return MapToDto(dbMovie);
        }
        else
        {
            return null;
        }
    }

    private List<TraktMovieDto> MapToDtos(List<TraktMovie> traktMovieList)
    {
        var traktMovieDtos = new List<TraktMovieDto>();
        foreach (var traktMovie in traktMovieList)
        {
           traktMovieDtos.Add(_mapper.Map<TraktMovie, TraktMovieDto>(traktMovie)); 
        }
        return traktMovieDtos;
    }

    public async Task<TraktMovieDto> UpdateAsync(Guid id, UpdateTraktMovieDto input)
    {
        var movie = await _traktMovieRepository.GetAsync(id);

        if (movie.Name != input.Name)
        {
            await _traktMovieManager.ChangeNameAsync(movie, input.Name);
        }

        movie.FirstAiredYear = input.FirstAiredYear;
        movie.ExternalId = input.ExternalId;

        await _traktMovieRepository.UpdateAsync(movie);
        return MapToDto(movie);
    }
    
    private async Task<TraktMovieDto> GetOneMovieAsync(Guid movieId)
    {
        var request = new SearchRequest { Id = movieId.ToString() };
        _logger.LogInformation("=== GRPC request {@request}", request);
        var response = await _movieGrpcServiceClient.SearchOneMovieAsync(request);
        _logger.LogInformation("=== GRPC response {@response}", response);
        return _mapper.Map<MovieModel, TraktMovieDto>(response) ??
               throw new UserFriendlyException(TraktServiceDomainErrorCodes.MovieNotFound);
    }
}