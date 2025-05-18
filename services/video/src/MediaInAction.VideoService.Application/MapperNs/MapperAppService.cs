using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediaInAction.VideoService.MapperNs.Dtos;
using MediaInAction.VideoService.Permissions;
using MediaInAction.VideoService.ToBeMappedNs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.MapperNs;

[Authorize(VideoServicePermissions.ToBeMappeds.Default)]
public class MapperAppService :  IMapperAppService
{
    private readonly ToBeMappedManager _toBeMappedManager;
    private readonly IToBeMappedRepository _toBeMappedRepository;
    private readonly ILogger<MapperAppService> _logger;
    
    public MapperAppService(ToBeMappedManager movieManager,
        IToBeMappedRepository movieRepository,
        ILogger<MapperAppService> logger
    )
    {
        _toBeMappedManager = movieManager;
        _toBeMappedRepository = movieRepository;
        _logger = logger;
    }
    
    [AllowAnonymous]
    public async Task<ToBeMappedDto> GetAsync(Guid id)
    {
        var movie = await _toBeMappedRepository.GetAsync(id);
        return null;
    }
    

    [AllowAnonymous]
    public async Task<ToBeMappedDto> CreateUpdateAsync(ToBeMappedCreateDto input)
    {
        var toBeMapped = await _toBeMappedManager.CreateToBeMappedAsync
        (
            alias: input.Alias
        );

        var toBe = new ToBeMappedDto();
        toBe.Alias = toBeMapped.Alias;
        toBe.Status = ToBeMappedStatus.New;

        return toBe;
    }
    
    [AllowAnonymous]
    public async Task<ToBeMappedDto> GetToBeMappedAsync(GetToBeMappedInput input)
    {
        var toBeDb = await _toBeMappedRepository.FindByAlias(input.Alias);
        var toBeOut = new ToBeMappedDto();
        toBeOut.Alias = toBeDb.Alias;
        toBeOut.Status = toBeDb.Status;
        return toBeOut;
    }

    [AllowAnonymous]
    public Task<PagedResultDto<ToBeMappedDto>> GetListPagedAsync(GetToBeMappedsInput input)
    {
        throw new NotImplementedException();
    }

    [Authorize(VideoServicePermissions.ToBeMappeds.Dashboard)]
    public async Task<ToBeMappedDashboardDto> GetDashboardAsync(
        ISpecification<ToBeMapped> spec, 
        CancellationToken cancellationToken = default)
    {
        return new ToBeMappedDashboardDto()
        {
            ToBeMappedStatusDto = await GetCountOfTotalToBeMappedStatusAsync(),
        };
    }

    private async Task<List<ToBeMappedStatusDto>> GetCountOfTotalToBeMappedStatusAsync()
    {
        var filter = "a:";
        ISpecification<ToBeMapped> specification = ToBeMappedNs.Specifications.SpecificationFactory.Create(filter);
        var toBeMappedList = await _toBeMappedRepository.GetDashboardAsync(specification);
        return CreateToBeMappedDtoMapping(toBeMappedList);
    }
    
    private List<ToBeMappedStatusDto> CreateToBeMappedDtoMapping(List<ToBeMapped> toBeMappedList)
    {
        var dtoList = new List<ToBeMappedStatusDto>();
        foreach (var toBeMapped in toBeMappedList)
        {
            var toBeMappedStatusDto = new ToBeMappedStatusDto();
            toBeMappedStatusDto.CountOfStatusToBeMapped = 1;
            toBeMappedStatusDto.ToBeMappedStatus = toBeMapped.Status.ToString();
            dtoList.Add(toBeMappedStatusDto);
        }
        return dtoList;
    }
}
