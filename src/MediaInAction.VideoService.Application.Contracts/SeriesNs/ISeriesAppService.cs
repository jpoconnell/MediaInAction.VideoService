using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MediaInAction.VideoService.SeriesNs;

public interface ISeriesAppService : IApplicationService
{
    Task<SeriesDto> CreateAsync(SeriesCreateDto input);
    Task<SeriesDto> GetAsync(Guid id);
    Task<List<SeriesDto>> GetMySeriessAsync(GetMySeriessInput input);
    Task<List<SeriesDto>> GetSeriessAsync(GetSeriessInput input);
    Task SetAsInActiveAsync(Guid id);
    Task<PagedResultDto<SeriesDto>> GetListPagedAsync(PagedAndSortedResultRequestDto input);
    Task<SeriesDashboardDto> GetDashboardAsync(DashboardInput input);

    Task<SeriesDto> GetByIdValue(string input);
    Task<SeriesDto> UpdateAsync(SeriesDto seriesDto);
}