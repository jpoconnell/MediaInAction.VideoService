using System.Threading.Tasks;
using MediaInAction.TraktService.TraktShowNs.Dtos;
using Volo.Abp.Application.Services;

namespace MediaInAction.TraktService.TraktShowNs;
public interface ITraktShowAppService : IApplicationService
{
    Task<TraktShowDashboardDto> GetDashboardAsync(TraktShowDashboardInput input);
    Task<TraktShowDto> CreateAsync(TraktShowCreateDto newEpisode);
}
