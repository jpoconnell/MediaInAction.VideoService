using System;
using System.Threading.Tasks;
using MediaInAction.VideoService.TorrentNs.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MediaInAction.VideoService.TorrentNs;
public interface ITorrentAppService : IApplicationService
{
    Task<TorrentDto> GetAsync(Guid id);
    Task<TorrentDto> CreateAsync(TorrentCreateDto input);
    Task<PagedResultDto<TorrentDto>> GetTorrentsAsync(GetTorrentsInput input);
    Task<TorrentDto> GetTorrentAsync(GetTorrentInput input);
   
}
