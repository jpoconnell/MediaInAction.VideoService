using System;
using System.Threading.Tasks;
using MediaInAction.VideoService.SeriesNs.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MediaInAction.VideoService.SeriesNs
{
    public interface IPublicSeriesAppService : IApplicationService
    {
        Task<ListResultDto<SeriesDto>> GetListAsync();
        Task<SeriesDto> GetAsync(Guid id);
    }
}