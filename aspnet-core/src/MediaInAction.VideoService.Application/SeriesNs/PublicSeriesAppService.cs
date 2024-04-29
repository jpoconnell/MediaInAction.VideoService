using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.SeriesNs.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace MediaInAction.VideoService.SeriesNs
{
    public class PublicSeriesAppService : ApplicationService, IPublicSeriesAppService
    {
        private readonly IRepository<Series, Guid> _productRepository;

        public PublicSeriesAppService(IRepository<Series, Guid> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ListResultDto<SeriesDto>> GetListAsync()
        {
            return new ListResultDto<SeriesDto>(
                ObjectMapper.Map<List<Series>, List<SeriesDto>>(
                    await _productRepository.GetListAsync()
                )
            );
        }
        
        public async Task<SeriesDto> GetAsync(Guid id)
        {
            var product = await _productRepository.GetAsync(id);
            return ObjectMapper.Map<Series, SeriesDto>(product);
        }
    }
}
