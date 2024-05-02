using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.SeriesAliasNs.Dtos;
using MediaInAction.VideoService.SeriesNs.Dtos;
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

        public async Task<List<SeriesDto>> GetListAsync()
        {
            var seriesList = await _seriesRepository.GetListAsync(true);
            var seriesDtoList = new  List<SeriesDto>();
            foreach (var series in seriesList)
            {
                var seriesDto = new SeriesDto();
                seriesDto.Id = series.Id;
                seriesDto.Name = series.Name;
                seriesDto.FirstAiredYear = series.FirstAiredYear;
                seriesDto.IsActive = series.IsActive;
                seriesDto.ImageName = series.ImageName;
                seriesDto.SeriesAliasDtos = new List<SeriesAliasDto>();
                foreach (var seriesAlias in series.SeriesAliases)
                {
                    var seriesAliasDto = new SeriesAliasDto();
                    seriesAliasDto.SeriesId = series.Id;
                    seriesAliasDto.IdType = seriesAlias.IdType;
                    seriesAliasDto.IdValue = seriesAlias.IdValue;
                    seriesDto.SeriesAliasDtos.Add(seriesAliasDto);
                }   
                seriesDtoList.Add(seriesDto);   
            }

            return seriesDtoList;
        }
        
        public async Task<SeriesDto> GetAsync(Guid id)
        {
            var product = await _seriesRepository.GetAsync(id);
            return ObjectMapper.Map<Series, SeriesDto>(product);
        }
    }
}
