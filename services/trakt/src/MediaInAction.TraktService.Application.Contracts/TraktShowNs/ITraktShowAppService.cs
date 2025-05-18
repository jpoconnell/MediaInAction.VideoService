using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.TraktService.TraktShowNs.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MediaInAction.TraktService.TraktShowNs;
public interface ITraktShowAppService : IApplicationService
{
    Task<TraktShowDashboardDto> GetDashboardAsync(TraktShowDashboardInput input);
    Task<TraktShowDto> CreateAsync(TraktShowCreateDto newEpisode);

    Task<PagedResultDto<TraktShowDto>> GetPagedListAsync(GetTraktShowListDto input);
    Task<List<TraktShowDto>> GetListAsync(GetTraktShowListDto input);
}
