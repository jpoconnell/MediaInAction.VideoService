using System;
using System.Threading;
using System.Threading.Tasks;
using MediaInAction.VideoService.MapperNs.Dtos;
using MediaInAction.VideoService.ToBeMappedNs;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.MapperNs;

public interface IMapperAppService : IApplicationService
{
    Task<ToBeMappedDto> GetAsync(Guid id);
    Task<ToBeMappedDto> CreateUpdateAsync(ToBeMappedCreateDto input);

    Task<ToBeMappedDto> GetToBeMappedAsync(GetToBeMappedInput input);
    Task<PagedResultDto<ToBeMappedDto>> GetListPagedAsync(GetToBeMappedsInput input);

    Task<ToBeMappedDashboardDto> GetDashboardAsync(
        ISpecification<ToBeMapped> spec,
        CancellationToken cancellationToken = default);
}