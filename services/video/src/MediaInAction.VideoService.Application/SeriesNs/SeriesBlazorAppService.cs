using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.Permissions;
using MediaInAction.VideoService.SeriesNs.Dtos;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace MediaInAction.VideoService.SeriesNs;

[Authorize(VideoServicePermissions.Seriess.Default)]
public class SeriesBlazorAppService : VideoServiceAppService, ISeriesBlazorAppService
{
    private readonly ISeriesRepository _seriesRepository;
    private readonly SeriesManager _seriesManager;

    public SeriesBlazorAppService(
        ISeriesRepository seriesRepository,
        SeriesManager seriesManager)
    {
        _seriesRepository = seriesRepository;
        _seriesManager = seriesManager;
    }

    public async Task<SeriesDto> GetAsync(Guid id)
    {
        var series = await _seriesRepository.GetAsync(id);
        return ObjectMapper.Map<Series, SeriesDto>(series);
    }

    public async Task<PagedResultDto<SeriesDto>> GetListAsync(GetSeriesListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(Series.Name);
        }

        var seriesList = await _seriesRepository.GetListAsync(
            input.SkipCount,
            input.MaxResultCount,
            input.Sorting,
            input.Filter
        );

        var totalCount = input.Filter == null
            ? await _seriesRepository.CountAsync()
            : await _seriesRepository.CountAsync(series => series.Name.Contains(input.Filter));

        return new PagedResultDto<SeriesDto>(
            totalCount,
            ObjectMapper.Map<List<Series>, List<SeriesDto>>(seriesList)
        );
    }

    [Authorize(VideoServicePermissions.Seriess.Create)]
    public async Task<SeriesDto> CreateAsync(CreateSeriesDto input)
    {
        var newSeries = new SeriesCreateDto
        {
            Name = input.Name,
            FirstAiredYear = input.FirstAiredYear,
            SeriesStatus = input.Status
        };

        var series = await _seriesManager.CreateUpdateAsync(newSeries);
        
        await _seriesRepository.InsertAsync(series);

        return ObjectMapper.Map<Series, SeriesDto>(series);
    }

    [Authorize(VideoServicePermissions.Seriess.Update)]
    public async Task UpdateAsync(Guid id, UpdateSeriesDto input)
    {
        var series = await _seriesRepository.GetAsync(id);

        if (series.Name != input.Name)
        {
            await _seriesManager.ChangeNameAsync(series, input.Name);
        }

        series.CreationTime = input.CreateDate;
        series.FirstAiredYear = input.FirstAiredYear;

        await _seriesRepository.UpdateAsync(series);
    }

    [Authorize(VideoServicePermissions.Seriess.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _seriesRepository.DeleteAsync(id);
    }
}