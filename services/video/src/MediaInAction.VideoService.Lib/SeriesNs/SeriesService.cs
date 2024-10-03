using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.SeriesAliasNs;
using Microsoft.Extensions.Logging;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.SeriesNs;

public class SeriesService: ISeriesService
{
    private readonly ISeriesRepository _seriesRepository;
    private readonly ILogger<SeriesService> _logger;
    
    public SeriesService(
        ISeriesRepository seriesRepository,
        ILogger<SeriesService>  logger)
    {
        _seriesRepository = seriesRepository;
        _logger = logger;
    }
    
    public async Task<SeriesDto> GetByIdAsync(Guid seriesId)
    {
        try
        {
            var series = await _seriesRepository.GetAsync(seriesId, true);
            return MapToSeriesDto(series);
        }
        catch (Exception ex)
        {
            _logger.LogDebug("SeriesService.GetByIdAsync" + ex.Message);
            return null;
        }
    }

    private SeriesDto MapToSeriesDto(Series series)
    {
        var seriesDto = new SeriesDto
        {
            Id = series.Id,
            Name = series.Name,
            FirstAiredYear = series.FirstAiredYear,
            IsActive = series.IsActive
        };
        
        if (series.SeriesAliases != null)
        {
            seriesDto.SeriesAliasDtos = new List<SeriesAliasDto>();
            foreach (var seriesAlias in series.SeriesAliases )
            {
                var newSeriesAliasDto = new SeriesAliasDto();
                newSeriesAliasDto.IdType = seriesAlias.IdType;
                newSeriesAliasDto.IdValue = seriesAlias.IdValue;
                newSeriesAliasDto.SeriesId = seriesAlias.SeriesId;
                seriesDto.SeriesAliasDtos.Add(newSeriesAliasDto);
            }
        }
        return seriesDto;
    }
    
    public async Task<List<SeriesDto>> GetActiveList()
    {
        // active list is 
        var filter = "a:" ;

        ISpecification<Series> specification = Specifications.SpecificationFactory.Create(filter);
        var seriesList = await _seriesRepository.GetSeriesBySpec(specification, true);
        var seriesDtos = new List<SeriesDto>();
        foreach (var series in seriesList)
        {
            var seriesDto = new SeriesDto
            {
                Id = series.Id,
                Name = series.Name,
                FirstAiredYear = series.FirstAiredYear,
                IsActive = series.IsActive
            };
            seriesDtos.Add(seriesDto);
        }

        return seriesDtos;
    }

    public Task<List<SeriesDto>> GetAllNoDefault()
    {
        throw new NotImplementedException();
    }

    public async Task<string> GetSlugAsync(Guid seriesId)
    {
        var seriesDto = await GetByIdAsync(seriesId);
        var slug = "";
        foreach (var seriesAliasDto in seriesDto.SeriesAliasDtos)
        {
            if (seriesAliasDto.IdType == "slug")
            {
                slug = seriesAliasDto.IdValue;
                break;
            }
        }

        return slug;
    }
}

