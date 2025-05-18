using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaInAction.TraktService.Permissions;
using MediaInAction.TraktService.TraktShowNs.Dtos;
using MediaInAction.TraktService.TraktShowNs.Specifications;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Specifications;

namespace MediaInAction.TraktService.TraktShowNs;

[Authorize(TraktServicePermissions.TraktShow.Default)]
public class TraktShowAppService : TraktServiceAppService, ITraktShowAppService
{
    private readonly ILogger<TraktShowAppService> _logger;
    private readonly ITraktShowRepository _traktShowRepository;
    private readonly TraktShowManager _traktShowManager;
    
    public TraktShowAppService(
        ITraktShowRepository traktShowRepository,
        ILogger<TraktShowAppService> logger,
        TraktShowManager traktShowManager)
    {
        _traktShowRepository = traktShowRepository;
        _traktShowManager = traktShowManager;
        _logger = logger;
    }
    
    [Authorize(TraktServicePermissions.TraktShow.Dashboard)]
    public async Task<TraktShowDashboardDto> GetDashboardAsync(TraktShowDashboardInput input)
    {
        return new TraktShowDashboardDto()
        {
            TraktShowStatusDto = await GetCountOfTotalShowStatusAsync(input.Filter)
        };
    }

    public async Task<TraktShowDto> GetAsync(Guid id)
    {
        var traktEpisode = await _traktShowRepository.GetAsync(id);
        return ObjectMapper.Map<TraktShow, TraktShowDto>(traktEpisode);
    }   

    public async Task<PagedResultDto<TraktShowDto>> GetPagedListAsync(GetTraktShowListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(TraktShow.Name);
        }

        var episodes = await _traktShowRepository.GetListAsync(
            input.SkipCount,
            input.MaxResultCount,
            input.Sorting,
            input.Filter
        );

        var totalCount = await _traktShowRepository.CountAsync();

        return new PagedResultDto<TraktShowDto>(
            totalCount,
            ObjectMapper.Map<List<TraktShow>, List<TraktShowDto>>(episodes)
        );
    }

    public async Task<List<TraktShowDto>> GetListAsync(GetTraktShowListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(TraktShow.Slug);
        }

        var shows = await _traktShowRepository.GetListAsync(
            input.SkipCount,
            input.MaxResultCount,
            input.Sorting,
            input.Filter
        );
        
        return new List<TraktShowDto>(
            ObjectMapper.Map<List<TraktShow>, List<TraktShowDto>>(shows)
        );
    }

    [Authorize(TraktServicePermissions.TraktShow.Create)]
    public async Task<TraktShowDto> CreateAsync(TraktShowCreateDto input)
    {
        var show = await _traktShowManager.CreateAsync(input);
        if (show != null)
        {
            var traktShowDto = MapToDto(show);
            // var traktShowDto = ObjectMapper.Map<TraktShow, TraktShowDto>(show);
            return traktShowDto;
        }
        else
        {
            return null;
        }
    }

    [Authorize(TraktServicePermissions.TraktShow.Update)]
    public async Task UpdateAsync(Guid id, UpdateTraktShowDto input)
    {
        var traktShow = await _traktShowRepository.GetAsync(id);
        traktShow.FirstAiredYear = input.FirstAiredYear;
        traktShow.ExternalId = input.ExternalId;
        await _traktShowRepository.UpdateAsync(traktShow);
    }

    [Authorize(TraktServicePermissions.TraktShow.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _traktShowRepository.DeleteAsync(id);
    }
    
    private async Task<List<TraktShowStatusDto>> GetCountOfTotalShowStatusAsync(string filter)
    {
        ISpecification<TraktShow> specification = SpecificationFactory.Create(filter);
        var shows = await _traktShowRepository.GetDashboardAsync(specification);
        return CreateShowStatusDtoMapping(shows);
    }

    private List<TraktShowStatusDto> CreateShowStatusDtoMapping(List<TraktShow> shows)
    {
        var showStatus = shows
            .GroupBy(p => p.Status)
            .Select(p => new TraktShowStatusDto { CountOfStatusShow = p.Count(), ShowStatus = p.Key.ToString() })
            .OrderBy(p => p.CountOfStatusShow)
            .ToList();

        return showStatus;
    }
    
    private TraktShowDto MapToDto(TraktShow show)
    {
        if (show.TraktShowAliases == null)
        {
            show.TraktShowAliases = new List<TraktShowAlias>();
            return null;
        }
        else
        {
            var returnTraktShowAliasDtoList = new List<TraktShowAliasDto>();
            foreach (var alias in show.TraktShowAliases)
            {
                var returnTraktShowAliasDto = new TraktShowAliasDto
                {
                    IdType = alias.IdType,
                    IdValue = alias.IdValue
                };
                returnTraktShowAliasDtoList.Add(returnTraktShowAliasDto);
            }
            var returnTraktShowDto = new TraktShowDto
            {
                Id = show.Id,
                Name = show.Name,
                FirstAiredYear = show.FirstAiredYear,
                Slug = show.Slug,
                TraktShowAliasDtos = returnTraktShowAliasDtoList
            };
            return returnTraktShowDto;
        }
    }

    public async Task<TraktShowDto> GetUniqueShow(TraktShowCreateDto traktShowCreateDto)
    {
        var traktShow = await _traktShowRepository.FindByTraktShowNameYearAsync(traktShowCreateDto.Name, traktShowCreateDto.FirstAiredYear);
        if (traktShow != null)
        {
            var traktShowDto = MapToDto(traktShow);
            return traktShowDto;
        }
        else
        {
            return null;
        }
    }
}