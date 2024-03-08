using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.SeriesNs.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MediaInAction.VideoService.SeriesNs;

    public interface ISeriesAppService : IApplicationService
    {
        Task<SeriesDto> CreateAsync(SeriesCreateDto input);
        Task<SeriesDto> GetAsync(Guid id);
        Task<SeriesDto> GetSeriesAsync(GetSeriesInput input);
        Task<List<SeriesDto>> GetSeriessAsync(GetSeriessInput input);
        Task<SeriesDto> GetBySeriesNameAsync(string name);
        Task SetAsInActiveAsync(Guid id);
        Task ExportInActiveAsync();
        Task ImportInActiveAsync();
        Task<PagedResultDto<SeriesDto>> GetListPagedAsync(PagedAndSortedResultRequestDto input);
        Task<DashboardDto> GetDashboardAsync(DashboardInput input);
        Task<SeriesDto> GetByIdValue(string slug);
    }
