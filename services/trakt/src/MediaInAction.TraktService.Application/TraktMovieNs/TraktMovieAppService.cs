using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaInAction.TraktService.Permissions;
using MediaInAction.TraktService.TraktMovieNs.Dtos;
using MediaInAction.TraktService.TraktMovieNs.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
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
    
    public async Task<TraktMovieDto> GetAsync(Guid id)
    {
        var traktMovie = await _traktMovieRepository.GetAsync(id);
        return ObjectMapper.Map<TraktMovie, TraktMovieDto>(traktMovie);
    }   
    
    public async Task<PagedResultDto<TraktMovieDto>> GetPagedListAsync(GetTraktMovieListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(TraktMovie.Name);
        }

        var movies = await _traktMovieRepository.GetListAsync(
            input.SkipCount,
            input.MaxResultCount,
            input.Sorting,
            input.Filter
        );

        var totalCount = await _traktMovieRepository.CountAsync();

        return new PagedResultDto<TraktMovieDto>(
            totalCount,
            ObjectMapper.Map<List<TraktMovie>, List<TraktMovieDto>>(movies)
        );
    }

    [Authorize(TraktServicePermissions.TraktMovie.Dashboard)]
    public async Task<MovieDashboardDto> GetDashboardAsync(MovieDashboardInput input)
    {
        return new MovieDashboardDto()
        {
            TraktMovieStatusDto = await GetCountOfTotalMovieStatusAsync(input.Filter),
        };
    }

    [Authorize(TraktServicePermissions.TraktMovie.Create)]
    public async Task<TraktMovieDto> CreateAsync(TraktMovieCreateDto input)
    {
        var movie = await _traktMovieManager.CreateAsync(input);
        if (movie == null)
        {
            return null;
        }
        else
        {
            var traktMovieDto = MapToDto(movie);
            return traktMovieDto;
        }
    }

    private TraktMovieDto MapToDto(TraktMovie movie)
    {
        var traktMovieAliasList = new List<TraktMovieAliasDto>();
        foreach (var movieAlias in movie.TraktMovieAliases)
        {
            var newTraktMovieAlias = new TraktMovieAliasDto();
            newTraktMovieAlias.IdType = movieAlias.IdType;
            newTraktMovieAlias.IdValue = movieAlias.IdValue;
            traktMovieAliasList.Add(newTraktMovieAlias);
        }
        var traktMovieDto = new TraktMovieDto();
        traktMovieDto.Id = movie.Id;
        traktMovieDto.Name = movie.Name;
        traktMovieDto.Slug = movie.Slug;
        traktMovieDto.MovieStatus = movie.Status;
        traktMovieDto.TraktMovieAliasDtos = traktMovieAliasList;

        return traktMovieDto;
    }

    public Task<List<TraktMovieDto>> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<TraktMovieDto> GetByNameYear(TraktMovieCreateDto traktMovieCreateDto)
    {
        var dbMovie = await _traktMovieRepository.GetByMovieNameYearAsync(
            traktMovieCreateDto.Name,traktMovieCreateDto.FirstAiredYear);
        return ObjectMapper.Map<TraktMovie, TraktMovieDto>(dbMovie);
    }

    [Authorize(TraktServicePermissions.TraktMovie.Update)]
    public async Task UpdateAsync(Guid id, UpdateTraktMovieDto input)
    {
        var movie = await _traktMovieRepository.GetAsync(id);

        if (movie.Name != input.Name)
        {
            await _traktMovieManager.ChangeNameAsync(movie, input.Name);
        }

        movie.FirstAiredYear = input.FirstAiredYear;
       // movie.ShortBio = input.ShortBio;

        await _traktMovieRepository.UpdateAsync(movie);
    }
    
    [Authorize(TraktServicePermissions.TraktMovie.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _traktMovieRepository.DeleteAsync(id);
    }

    private async Task<List<TraktMovieStatusDto>> GetCountOfTotalMovieStatusAsync(string filter)
    {
        ISpecification<TraktMovie> specification = SpecificationFactory.Create(filter);
        var movies = await _traktMovieRepository.GetDashboardAsync(specification);
        return CreateMovieStatusDtoMapping(movies);
    }
    
    private List<TraktMovieStatusDto> CreateMovieStatusDtoMapping(List<TraktMovie> movies)
    {
        var movieStatus = movies
            .GroupBy(p => p.Status)
            .Select(p => new TraktMovieStatusDto { CountOfStatusMovie = p.Count(), MovieStatus = p.Key.ToString() })
            .OrderBy(p => p.CountOfStatusMovie)
            .ToList();

        return movieStatus;
    }
}