using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.EpisodeNs.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace MediaInAction.VideoService.EpisodeNs
{
    public class PublicEpisodeAppService : ApplicationService, IPublicEpisodeAppService
    {
        private readonly IRepository<Episode, Guid> _episodeRepository;
        private readonly IObjectMapper _objectMapper;
        
        public PublicEpisodeAppService(IRepository<Episode, Guid> episodeRepository,  IObjectMapper objectMapper)
        {
            _episodeRepository = episodeRepository;
            _objectMapper = objectMapper;
        }

        public async Task<ListResultDto<EpisodeDto>> GetListAsync()
        {
            return new ListResultDto<EpisodeDto>(
                ObjectMapper.Map<List<Episode>, List<EpisodeDto>>(
                    await _episodeRepository.GetListAsync()
                )
            );
        }
        
        public async Task<EpisodeDto> GetAsync(Guid id)
        {
            var product = await _episodeRepository.GetAsync(id);
            return ObjectMapper.Map<Episode, EpisodeDto>(product);
        }
    }
}
