using System;
using System.Threading.Tasks;
using MediaInAction.VideoService.MovieNs.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace MediaInAction.VideoService.MovieNs
{
    public interface IPublicMovieAppService : IApplicationService
    {
        Task<ListResultDto<MovieDto>> GetListAsync();
        Task<MovieDto> GetAsync(Guid id);
    }
}