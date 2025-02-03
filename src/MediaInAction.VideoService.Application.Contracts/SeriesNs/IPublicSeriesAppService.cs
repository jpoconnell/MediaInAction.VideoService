using System;
using System.Threading.Tasks;
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