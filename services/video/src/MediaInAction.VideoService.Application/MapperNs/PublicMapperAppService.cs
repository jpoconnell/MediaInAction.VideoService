using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.MapperNs.Dtos;
using MediaInAction.VideoService.ToBeMappedNs;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MediaInAction.VideoService.MapperNs
{
    public class PublicMapperAppService : ApplicationService, IPublicMapperAppService
    {
        private readonly IToBeMappedRepository _toBeMappedRepository;

        public PublicMapperAppService(IToBeMappedRepository tpBeMappedRepository)
        {
            _toBeMappedRepository = tpBeMappedRepository;
        }

        public async Task<ListResultDto<MapperDto>> GetListAsync()
        {
            return new ListResultDto<MapperDto>(
                ObjectMapper.Map<List<ToBeMapped>, List<MapperDto>>(
                    await _toBeMappedRepository.GetListAsync()
                )
            );
        }
        
        public async Task<MapperDto> GetAsync(Guid id)
        {
            var tpBeMapped = await _toBeMappedRepository.GetAsync(id);
            return ObjectMapper.Map<ToBeMapped, MapperDto>(tpBeMapped);
        }
    }
}
