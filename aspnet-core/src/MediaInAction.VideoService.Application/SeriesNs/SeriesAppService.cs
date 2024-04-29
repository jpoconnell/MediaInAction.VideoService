using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaInAction.VideoService.Permissions;
using MediaInAction.VideoService.SeriesAliasNs;
using MediaInAction.VideoService.SeriesAliasNs.Dtos;
using MediaInAction.VideoService.SeriesNs.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.SeriesNs;

public class SeriesAppService : VideoServiceAppService, ISeriesAppService
{
    private readonly ISeriesRepository _seriesRepository;
    private readonly SeriesManager _seriesManager;
    private readonly ISeriesAliasRepository _seriesAliasRepository;
    private readonly ILogger<SeriesAppService> _logger;

    public SeriesAppService(ISeriesRepository seriesRepository,
        ILogger<SeriesAppService> logger,
        SeriesManager seriesManager,
        ISeriesAliasRepository seriesAliasRepository,
        SeriesAliasManager seriesAliasManager)
    {
        _seriesRepository = seriesRepository;
        _seriesManager = seriesManager;
        _seriesAliasRepository = seriesAliasRepository;
        _logger = logger;
    }

    public async Task<SeriesDto> GetAsync(Guid id)
    {
        var series = await _seriesRepository.GetAsync(id);
        return ObjectMapper.Map<Series, SeriesDto>(series);
    }

    async Task<SeriesDto> ISeriesAppService.GetSeriesAsync(GetSeriesInput input)
    {
        ISpecification<Series> specification = Specifications.SpecificationFactory.Create(input.Filter);
        var series = await _seriesRepository.GetSeriesBySpec(specification, true);
        if (series.Count == 1)
        {
            return CreateSeriesDtoMapping(series[0]);
        }
        else
        {
            return null;
        }
    }

    public Task<List<SeriesDto>> GetSeriessAsync(GetSeriessInput input)
    {
        throw new NotImplementedException();
    }

    [AllowAnonymous]
    public async Task<SeriesDto> CreateAsync(SeriesCreateDto input)
    {
        var seriesAliases = GetSeriesAliasTuple(input.SeriesAliases);

        var series = await _seriesManager.CreateAsync
        (
            name: input.Name,
            year: input.FirstAiredYear,
            seriesAliases: seriesAliases
        );

        return CreateSeriesDtoMapping(series);
    }

    public async Task<SeriesDto> GetSeriesAsync(GetSeriesInput input)
    {
        ISpecification<Series> specification = Specifications.SpecificationFactory.Create(input.Filter);
        var series = await _seriesRepository.GetSeriesBySpec(specification, true);
        return null;
        //CreateSeriesDtoMapping(series);
    }

    [AllowAnonymous]
    public async Task<SeriesDto> GetBySeriesNameAsync(string name)
    {
        var seriesList = await _seriesRepository.GetBySeriesName(name);
        Logger.LogInformation($" Series recieved with name of:{name}");
        return CreateSeriesDtoMapping(seriesList[0]);
    }

    public async Task<PagedResultDto<SeriesDto>> GetListPagedAsync(PagedAndSortedResultRequestDto input)
    {
        ISpecification<Series> specification = Specifications.SpecificationFactory.Create("a:");

        var seriess =
            await _seriesRepository.GetListPagedAsync(specification, input.SkipCount,
                input.MaxResultCount, "SeriesName" );

        var totalCount = await _seriesRepository.GetCountAsync();
        _logger.LogInformation("Count:" +totalCount.ToString());
        return new PagedResultDto<SeriesDto>(
            totalCount,
            CreateSeriesDtoMapping(seriess)
        );
    }

    public async Task<DashboardDto> GetDashboardAsync(DashboardInput input)
    {
        return new DashboardDto()
        {
            SeriesStatusDto = await GetCountOfTotalSeriesStatusAsync(input.Filter)
        };
    }

    [Authorize(VideoServicePermissions.Seriess.SetAsInActive)]
    public async Task SetAsInActiveAsync(Guid id)
    {
        await _seriesManager.SetAsInActiveAsync(id);
    }
    
    [Authorize(VideoServicePermissions.Seriess.Update)]
    public async Task<SeriesDto> UpdateAsync(Guid id, SeriesDto input)
    {
        var series = await _seriesRepository.GetAsync(id);
        series.Name = input.Name;
        series.FirstAiredYear = input.FirstAiredYear;
        series.IsActive = input.IsActive;
        await _seriesRepository.UpdateAsync(series, true);

        return ObjectMapper.Map<Series, SeriesDto>(series);
    }
    
    public async Task<SeriesDto> GetByIdValue(string slug)
    {
        _logger.LogInformation("GetByIdValue:" + slug);
        try
        {
            if (!slug.IsNullOrEmpty())
            {
                _logger.LogInformation("GetByIdValue1" );
                var seriesAlias = await _seriesAliasRepository.GetByIdValue(slug);
                _logger.LogInformation("GetByIdValue2" );
                if (seriesAlias != null)
                {
                    _logger.LogInformation("GetByIdValue3" );
                    var series = await _seriesRepository.GetAsync(seriesAlias.SeriesId);
                    var seriesDto = CreateSeriesDtoMapping(series);
                    return seriesDto;
                }
                else
                {
                    _logger.LogInformation("slug not found:" + slug);
                    return null;
                }
            }
            _logger.LogInformation("slug has no value");
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogDebug("GetByIdValue Error:" + ex.Message);
            return null;
        }
    }

    private async Task<List<SeriesStatusDto>> GetCountOfTotalSeriesStatusAsync(
        string inputFilter)
    {
        ISpecification<Series> specification = Specifications.SpecificationFactory.Create(inputFilter);
        var movies = await _seriesRepository.GetDashboardAsync(specification);
        return CreateSeriesStatusDtoMapping(movies);
    }

    private List<SeriesStatusDto> CreateSeriesStatusDtoMapping(List<Series> seriess)
    {
        var movieStatus = seriess
            .GroupBy(p => p.IsActive)
            .Select(p => new SeriesStatusDto { CountOfStatusSeries = p.Count(), IsActive = p.Key.ToString() })
            .OrderBy(p => p.CountOfStatusSeries)
            .ToList();

        return movieStatus;
    }

    private List<SeriesDto> CreateSeriesDtoMapping(List<Series> seriess)
    {
        _logger.LogInformation("CreateSeriesDtoMapping");
        List<SeriesDto> dtoList = new List<SeriesDto>();
        foreach (var series in seriess)
        {
            dtoList.Add(CreateSeriesDtoMapping(series));
        }

        return dtoList;
    }

    private SeriesDto CreateSeriesDtoMapping(Series series)
    {
        var seriesAliases = new List<SeriesAliasDto>();
        foreach (var sa in series.SeriesAliases)
        {
            var newSa = new SeriesAliasDto();
            newSa.SeriesId = sa.SeriesId;
            newSa.IdType = sa.IdType;
            newSa.IdValue = sa.IdValue;
            seriesAliases.Add(newSa);
        }

        return new SeriesDto()
        {
            Id = series.Id,
            Name = series.Name,
            IsActive = series.IsActive,
            FirstAiredYear = series.FirstAiredYear,
            SeriesAliasDtos = seriesAliases
        };
    }
    
    private List<( Guid? seriesId, string idType, string idValue
        )> GetSeriesAliasTuple(List<SeriesAliasCreateDto> inSeriesAliases)
    {
        var seriesAliases =
            new List<( Guid? seriesId, string idType, string idValue)>();
        foreach (var seriesAlias in inSeriesAliases)
        {
            seriesAliases.Add(( seriesAlias.SeriesId, seriesAlias.IdType, seriesAlias.IdValue ));
        }
        return seriesAliases;
    }
}
