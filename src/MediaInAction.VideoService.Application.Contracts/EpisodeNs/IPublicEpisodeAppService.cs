using System;
using System.Threading.Tasks;
using MediaInAction.VideoService.EpisodeNs;
using MediaInAction.VideoService.EpisodeNs.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MediaInAction.VideoService.EpisodeNs
{
    public interface IPublicEpisodeAppService : IApplicationService
    {
        Task<ListResultDto<EpisodeDto>> GetListAsync();
        Task<EpisodeDto> GetAsync(Guid id);
    }
}