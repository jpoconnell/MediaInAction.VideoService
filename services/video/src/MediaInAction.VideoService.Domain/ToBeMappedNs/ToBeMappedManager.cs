﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Distributed;

namespace MediaInAction.VideoService.ToBeMappedNs;
public class ToBeMappedManager : DomainService
{
    private readonly IToBeMappedRepository _toBeMappedRepository;
    private readonly ILogger<ToBeMappedManager> _logger;
    
    public ToBeMappedManager(IToBeMappedRepository toBeMappedRepository,
        ILogger<ToBeMappedManager> logger,
        IDistributedEventBus distributedEventBus)
    {
        _toBeMappedRepository = toBeMappedRepository;
        _logger = logger;
    }
    
    public async Task<ToBeMapped> CreateAsync(ToBeMappedCreateDto toBeMappedCreateDto)
    {
        try 
        {
            var myAlias = toBeMappedCreateDto.Alias.ToLower();
            // Create new toBeMapped
            ToBeMapped toBeMapped = new ToBeMapped(
                id: GuidGenerator.Create(),
                alias: myAlias,
                processed: ToBeMappedStatus.New,
                tries: 0
            );
            var dbToBeMapped = await _toBeMappedRepository.FindByAlias(toBeMapped.Alias);

            if (dbToBeMapped == null)
            {
                var createdToBeMapped = await _toBeMappedRepository.InsertAsync(toBeMapped, true);
                return createdToBeMapped;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return null;
        }
        return null;
    }


    public async Task<ToBeMapped> CreateToBeMappedAsync(
        string alias
    )
    {
        var myAlias = alias.ToLower();
        // Create new toBeMapped
        ToBeMapped toBeMapped = new ToBeMapped(
            id: GuidGenerator.Create(),
            alias: myAlias,
            tries: 0
        );
        var dbToBeMapped = await _toBeMappedRepository.FindByAlias(toBeMapped.Alias);

        if (dbToBeMapped == null)
        {
            var createdToBeMapped = await _toBeMappedRepository.InsertAsync(toBeMapped, true);
            return createdToBeMapped;
        }

        return toBeMapped;
    }
}
