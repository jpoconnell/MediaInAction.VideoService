using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.SeriesNs;
using MediaInAction.VideoService.ToBeMappedNs.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MediaInAction.VideoService.ToBeMappedNs;
public interface IToBeMappedAppService : IApplicationService
{
    Task<Dtos.ToBeMappedDto> GetAsync(Guid id);
    Task<Dtos.ToBeMappedDto> CreateAsync(ToBeMappedCreateDto input);

    Task<List<Dtos.ToBeMappedDto>> GetToBeMappedsAsync(GetToBeMappedsInput input);
    Task<Dtos.ToBeMappedDto> GetToBeMappedAsync(GetToBeMappedInput input);
    Task<PagedResultDto<Dtos.ToBeMappedDto>> GetListPagedAsync(GetToBeMappedsInput input);
    Task<object> GetDashboardAsync(DashboardInput dashboardInput);
}
