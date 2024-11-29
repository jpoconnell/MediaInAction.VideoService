using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.ToBeMappedNs;
using Microsoft.Extensions.Logging;
using NUglify.JavaScript.Syntax;

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

    public async Task UpdateAsync(ToBeMappedDto toBeMapped)
    {
        await _toBeMappedManager.CreateToBeMappedAsync(toBeMapped);
    }

    public async Task<List<ToBeMappedDto>> GetUnMapped(int i)
    {
        _logger.LogInformation($"GetUnMapped {i}");
        var unMapped = await _toBeMappedRepository.GetUnMapped(i);
       if (unMapped is null)
       {
           return null;
       }
       else 
       {
            return MapToDto(unMapped);
       }
    }

    private List<ToBeMappedDto> MapToDto(List<ToBeMapped> unMapped)
    {
        var toBeMappedDtos = new List<ToBeMappedDto>();
        foreach (var toBeMapped in unMapped)
        {
            var toBeMappedDto = new ToBeMappedDto();
            toBeMappedDto.Alias = toBeMapped.Alias;
            toBeMappedDto.Processed = toBeMapped.Processed;  
            toBeMappedDtos.Add(toBeMappedDto);
        }
        return toBeMappedDtos;
    }

    public async Task<List<ToBeMappedDto>> GetNotProcessed()
    {
        var toBeMappeds = await _toBeMappedRepository.GetUnMapped(1000);

        return Translating(toBeMappeds);
    }

    private List<ToBeMappedDto> Translating(List<ToBeMapped> toBeMappeds)
    {
        throw new NotImplementedException();
    }
  
}

