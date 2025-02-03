using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace MediaInAction.VideoService.SeriesNs
{
    public class PublicSeriesAppService : ApplicationService, IPublicSeriesAppService
    {
        private readonly IRepository<Series, Guid> _seriesRepository;

        public PublicSeriesAppService(IRepository<Series, Guid> seriesRepository)
        {
            _seriesRepository = seriesRepository;
        }

        public async Task<ListResultDto<SeriesDto>> GetListAsync()
        {
            return new ListResultDto<SeriesDto>(
                ObjectMapper.Map<List<Series>, List<SeriesDto>>(
                    await _seriesRepository.GetListAsync()
                )
            );
        }
        
        public async Task<SeriesDto> GetAsync(Guid id)
        {
            var series = await _seriesRepository.GetAsync(id);
            return ObjectMapper.Map<Series, SeriesDto>(series);
        }
    }
}
