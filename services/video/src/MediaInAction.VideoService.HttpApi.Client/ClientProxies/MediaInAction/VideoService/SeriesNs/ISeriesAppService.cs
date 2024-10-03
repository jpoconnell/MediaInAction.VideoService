// This file is automatically generated by ABP framework to use MVC Controllers from CSharp
using MediaInAction.VideoService.SeriesNs;
using MediaInAction.VideoService.SeriesNs.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

// ReSharper disable once CheckNamespace
namespace MediaInAction.VideoService.SeriesNs;

public interface ISeriesAppService : IApplicationService
{
    Task<SeriesDto> CreateAsync(SeriesCreateDto input);

    Task<SeriesDto> GetAsync(Guid id);

    Task<SeriesDto> GetSeriesAsync(GetSeriesInput input);

    Task SetAsInActiveAsync(Guid id);

    Task<PagedResultDto<SeriesDto>> GetSeriesListPagedAsync(GetSeriesListInput input);

    Task<DashboardDto> GetSeriesDashboardAsync(DashboardInput input);

    Task ExportSeriesDataAsync();

    Task<List<SeriesDto>> GetSeriesListAsync(GetSeriesListInput input);
}
