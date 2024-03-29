﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.Permissions;
using MediaInAction.VideoService.ToBeMappedNs.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace MediaInAction.VideoService.ToBeMappedNs;

[Authorize(VideoServicePermissions.ToBeMappeds.Default)]
public class ToBeMappedAppService : VideoServiceAppService, IToBeMappedAppService
{
    private readonly ToBeMappedManager _toBeMappedManager;
    private readonly IToBeMappedRepository _toBeMappedRepository;
    private readonly ILogger<ToBeMappedAppService> _logger;
    
    public ToBeMappedAppService(ToBeMappedManager movieManager,
        IToBeMappedRepository movieRepository,
        ILogger<ToBeMappedAppService> logger
    )
    {
        _toBeMappedManager = movieManager;
        _toBeMappedRepository = movieRepository;
        _logger = logger;
    }
    
    public async Task<ToBeMappedDto> GetAsync(Guid id)
    {
        var movie = await _toBeMappedRepository.GetAsync(id);
        return null;
    }
    
    
    public async Task<ToBeMappedDto> CreateAsync(ToBeMappedCreateDto input)
    {
        var toBeMapped = await _toBeMappedManager.CreateToBeMappedAsync
        (
            alias: input.Alias
        );

        var toBe = new ToBeMappedDto();
        toBe.Alias = toBeMapped.Alias;
        toBe.Processed = false;

        return toBe;
    }
    
    public Task<List<ToBeMappedDto>> GetToBeMappedsAsync(GetToBeMappedsInput getToBeMappedInput)
    {
        throw new NotImplementedException();
    }

    public async Task<ToBeMappedDto> GetToBeMappedAsync(GetToBeMappedInput input)
    {
        var toBeDb = await _toBeMappedRepository.FindByAlias(input.Alias);
        var toBeOut = new ToBeMappedDto();
        toBeOut.Alias = toBeDb.Alias;
        toBeOut.Processed = toBeDb.Processed;
        return toBeOut;
    }
}
