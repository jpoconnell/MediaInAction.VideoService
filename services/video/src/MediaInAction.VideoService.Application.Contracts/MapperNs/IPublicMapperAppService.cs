using System;
using System.Threading.Tasks;
using MediaInAction.VideoService.MapperNs.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MediaInAction.VideoService.MapperNs
{
    public interface IPublicMapperAppService : IApplicationService
    {
        Task<ListResultDto<MapperDto>> GetListAsync();
        Task<MapperDto> GetAsync(Guid id);
    }
}