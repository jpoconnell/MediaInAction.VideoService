using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MediaInAction.VideoService.SeriesNs
{
    public class PublicSeriesAppService : ApplicationService, IPublicSeriesAppService
    {
        private readonly ISeriesRepository _seriesRepository;

        public PublicSeriesAppService(ISeriesRepository seriesRepository)
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
