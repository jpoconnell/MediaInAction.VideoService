using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.EpisodeNs;
using MediaInAction.VideoService.EpisodeNs.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace MediaInAction.VideoService.EpisodeNs
{
    public class PublicEpisodeAppService : ApplicationService, IPublicEpisodeAppService
    {
        private readonly IRepository<Episode, Guid> _seriesRepository;

        public PublicEpisodeAppService(IRepository<Episode, Guid> seriesRepository)
        {
            _seriesRepository = seriesRepository;
        }

        public async Task<ListResultDto<EpisodeDto>> GetListAsync()
        {
            return new ListResultDto<EpisodeDto>(
                ObjectMapper.Map<List<Episode>, List<EpisodeDto>>(
                    await _seriesRepository.GetListAsync()
                )
            );
        }
        
        public async Task<EpisodeDto> GetAsync(Guid id)
        {
            var series = await _seriesRepository.GetAsync(id);
            return ObjectMapper.Map<Episode, EpisodeDto>(series);
        }
    }
}
