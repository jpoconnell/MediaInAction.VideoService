using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.TraktService.Permissions;
using MediaInAction.TraktService.TraktMovieNs.Dtos;
using MediaInAction.TraktService.TraktMovieNs.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Volo.Abp.Specifications;

namespace MediaInAction.TraktService.TraktMovieNs;

[Authorize(TraktServicePermissions.TraktMovie.Default)]
public class TraktMovieAppService : TraktServiceAppService, ITraktMovieAppService
{
    private readonly ILogger<TraktMovieAppService> _logger;
    private readonly ITraktMovieRepository _traktMovieRepository;
    private readonly TraktMovieManager _traktMovieManager;

    public TraktMovieAppService(
        ITraktMovieRepository movieRepository,
        ILogger<TraktMovieAppService>  logger,
        TraktMovieManager traktMovieManager)
    {
        _traktMovieRepository = movieRepository;
        _traktMovieManager = traktMovieManager;
        _logger = logger;
    }
    
    [Authorize(TraktServicePermissions.TraktMovie.Dashboard)]
    public async Task<MovieDashboardDto> GetDashboardAsync(MovieDashboardInput input)
    {
        return new MovieDashboardDto()
        {
            TraktMovieStatusDto = await GetCountOfTotalMovieStatusAsync(input.Filter),
        };
    }

    public async Task<TraktMovieDto> CreateAsync(TraktMovieCreateDto input)
    { 
        var movie = await _traktMovieManager.CreateAsync(input);
        if (movie != null)
        {
            var traktShowDto = MapToDto(movie);
            //ObjectMapper.Map<TraktMovie, TraktMovieDto>(movie);
            return traktShowDto;
        }
        else return null;
    }

    private TraktMovieDto MapToDto(TraktMovie movie)
    {
        var traktMovieAlaisDtoList = new List<TraktMovieAliasDto>();
        foreach (var traktAlias in movie.TraktMovieAliases)
        {
            var newTraktAlias = new TraktMovieAliasDto();
            newTraktAlias.IdType = traktAlias.IdType;
            newTraktAlias.IdValue = traktAlias.IdValue;
            traktMovieAlaisDtoList.Add(newTraktAlias);
        }
        
        var traktMovieDto = new TraktMovieDto();
        traktMovieDto.Id = movie.Id;
        traktMovieDto.Name = movie.Name;
        traktMovieDto.FirstAiredYear = movie.FirstAiredYear;
        traktMovieDto.TraktMovieAliasDtos = traktMovieAlaisDtoList;
        return traktMovieDto;
    }

    private async Task<List<TraktMovieStatusDto>> GetCountOfTotalMovieStatusAsync(string filter)
    {
        ISpecification<TraktMovie> specification = SpecificationFactory.Create(filter);
        var movies = await _traktMovieRepository.GetDashboardAsync(specification);
        return CreateMovieStatusDtoMapping(movies);
    }

    private List<TraktMovieStatusDto> CreateMovieStatusDtoMapping(List<TraktMovie> movies)
    {
        throw new System.NotImplementedException();
    }
}