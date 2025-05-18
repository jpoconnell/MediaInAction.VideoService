using System;
using System.Threading.Tasks;
using MediaInAction.VideoService.SeriesNs.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MediaInAction.VideoService.SeriesNs;

public interface ISeriesBlazorAppService : IApplicationService
{
    Task<SeriesDto> GetAsync(Guid id);

    Task<PagedResultDto<SeriesDto>> GetListAsync(GetSeriesListDto input);

    Task<SeriesDto> CreateAsync(CreateSeriesDto input);

    Task UpdateAsync(Guid id, UpdateSeriesDto input);

    Task DeleteAsync(Guid id);
}
