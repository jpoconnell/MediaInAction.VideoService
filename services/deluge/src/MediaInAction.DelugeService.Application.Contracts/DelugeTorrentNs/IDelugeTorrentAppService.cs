using System.Threading.Tasks;
using MediaInAction.DelugeService.DelugeTorrentNs.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MediaInAction.DelugeService.DelugeTorrentNs
{
    public interface IDelugeTorrentAppService : IApplicationService
    {
        Task<PagedResultDto<DelugeTorrentDto>> GetTorrentListPagedAsync(GetTorrentListDto filter);
    }
}