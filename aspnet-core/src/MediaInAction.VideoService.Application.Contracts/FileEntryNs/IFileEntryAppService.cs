using System;
using System.Threading.Tasks;
using MediaInAction.VideoService.FileEntryNs.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MediaInAction.VideoService.FileEntryNs;

public interface IFileEntryAppService : IApplicationService
{
    Task<FileEntryDto> GetAsync(Guid id);
    Task<FileEntryDto> CreateAsync(FileEntryCreatedDto input);
    Task<FileEntryDto> GetFileEntryAsync(GetFileEntryInput input);
    Task SetAsMappedAsync(Guid id);
    Task<PagedResultDto<FileEntryDto>> GetListPagedAsync(PagedAndSortedResultRequestDto input);
    Task<DashboardDto> GetDashboardAsync(DashboardInput input);
}
