using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.Localization;
using MediaInAction.VideoService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.SeriesNs;

[Authorize(VideoServicePermissions.Seriess.Default)]
public class SeriesAppService : ApplicationService, ISeriesAppService
{
    private readonly SeriesManager _seriesManager;
    private readonly ISeriesRepository _seriesRepository;

    public SeriesAppService(SeriesManager seriesManager,
        ISeriesRepository seriesRepository
    )
    {
        _seriesManager = seriesManager;
        _seriesRepository = seriesRepository;
        LocalizationResource = typeof(VideoServiceResource);
        ObjectMapperContext = typeof(VideoServiceApplicationModule);
    }

    [Authorize(VideoServicePermissions.Seriess.Create)]
    public async Task<SeriesDto> CreateAsync(SeriesCreateDto input)
    {
        var createSeries = await _seriesManager.CreateUpdateAsync(input);
        return CreateSeriesDtoMapping(createSeries);
    }
    
    [Authorize(VideoServicePermissions.Seriess.Dashboard)]
    public async Task<SeriesDashboardDto> GetDashboardAsync(DashboardInput input)
    {
        return new SeriesDashboardDto()
        {
            SeriesStatusDto = await GetCountOfTotalSeriesStatusAsync(input.Filter),
           // SeriesIsActiveDto = await GetCountOfTotalSeriesIsActiveAsync(input.Filter),
        };
    }

    [Authorize(VideoServicePermissions.Seriess.SetAsInActive)]
    public async Task SetAsInActiveAsync(Guid id)
    {
        var series = await _seriesRepository.GetAsync(id);
        series.SetSeriesAsInActive();
        await _seriesRepository.UpdateAsync(series);
    }
    private async Task GetCountOfTotalSeriesIsActiveAsync(string filter)
    {
        ISpecification<Series> specification = Specifications.SpecificationFactory.Create(filter);
        var series = await _seriesRepository.GetDashboardAsync(specification, false);
        //return CreateIsActiveDtoMapping(series);
    }

    private Task<List<SeriesIsActiveDto>> CreateIsActiveDtoMapping(List<Series> series) 
    {
        throw new NotImplementedException();
    }

    public Task<SeriesDto> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<SeriesDto>> GetMySeriessAsync(GetMySeriessInput input)
    {
        throw new NotImplementedException();
    }

    public Task<List<SeriesDto>> GetSeriessAsync(GetSeriessInput input)
    {
        throw new NotImplementedException();
    }
    
    public async Task<PagedResultDto<SeriesDto>> GetListPagedAsync(PagedAndSortedResultRequestDto input)
    {
        var orders = await _seriesRepository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, input.Sorting ?? "OrderDate", true);

        var totalCount = await _seriesRepository.GetCountAsync();
        return new PagedResultDto<SeriesDto>(
            totalCount,
            CreateSeriesDtoMapping(orders)
        );
    }
    
    private async Task<List<SeriesStatusDto>> GetCountOfTotalSeriesStatusAsync(string filter)
    {
        ISpecification<Series> specification = Specifications.SpecificationFactory.Create(filter);
        var series = await _seriesRepository.GetDashboardAsync(specification);
        return CreateSeriesStatusDtoMapping(series);
    }

    private List<SeriesStatusDto> CreateSeriesStatusDtoMapping(List<Series> orders)
    {
        throw new NotImplementedException();
    }
    
    private List<SeriesDto> CreateSeriesDtoMapping(List<Series> seriess)
    {
        List<SeriesDto> dtoList = new List<SeriesDto>();
        foreach (var series in seriess)
        {
            dtoList.Add(CreateSeriesDtoMapping(series));
        }

        return dtoList;
    }
    
    private SeriesDto CreateSeriesDtoMapping(Series series)
    {
        return new SeriesDto()
        {
            SeriesAliasDtos = ObjectMapper.Map<List<SeriesAlias>, List<SeriesAliasDto>>(series.SeriesAliases),
            Id = series.Id,
            SeriesName = series.Name,
            FirstAiredYear = series.FirstAiredYear,
            SeriesStatus = series.SeriesStatus
        };
    }

    public Task<SeriesDto> GetByIdValue(string input)
    {
        throw new NotImplementedException();
    }

    public Task<SeriesDto> UpdateAsync(SeriesDto seriesDto)
    {
        throw new NotImplementedException();
    }

    private List<(Guid seriesId, string idType, string idValue
        )> GetSeriesAliasTuple(List<SeriesAliasCreateDto> seriesAliasCreateDtos)
    {
        var seriesAliases =
            new List<(Guid seriesId, string idType, string idValue)>();
        foreach (var seriesAlias in seriesAliasCreateDtos)
        {
            seriesAliases.Add((seriesAlias.SeriesId, seriesAlias.IdType, seriesAlias.IdValue));
        }

        return seriesAliases;
    }
}