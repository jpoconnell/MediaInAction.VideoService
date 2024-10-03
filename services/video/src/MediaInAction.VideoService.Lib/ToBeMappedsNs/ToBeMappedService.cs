using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.ToBeMappedNs;
using Microsoft.Extensions.Logging;

namespace MediaInAction.VideoService.ToBeMappedsNs;

public class ToBeMappedService: IToBeMappedService
{
    private readonly IToBeMappedRepository _toBeMappedRepository;
    private readonly ToBeMappedManager _toBeMappedManager;
    private readonly ILogger<ToBeMappedService> _logger;
    public ToBeMappedService(
        IToBeMappedRepository toBeMappedRepository,
        ILogger<ToBeMappedService> logger,
        ToBeMappedManager toBeMappedManager)
    {
        _toBeMappedRepository = toBeMappedRepository;
        _toBeMappedManager = toBeMappedManager;
        _logger = logger;
    }

    public async Task CreateToBeMappedASync(string alias)
    {
        await _toBeMappedManager.CreateToBeMappedAsync(alias);
    }

    public Task UpdateAsync(ToBeMappedDto toBeMapped)
    {
        throw new NotImplementedException();
    }

    public async Task<List<ToBeMappedDto>> GetNotProcessed()
    {
        var toBeMappeds = await _toBeMappedRepository.GetMapped(false,1000);

        return Translating(toBeMappeds);
    }

    private List<ToBeMappedDto> Translating(List<ToBeMapped> toBeMappeds)
    {
        throw new NotImplementedException();
    }
    

    public Task<List<ToBeMappedDto>> GetMapped(bool b, int limit)
    {
        throw new NotImplementedException();
    }
}

