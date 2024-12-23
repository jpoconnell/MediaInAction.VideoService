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
                processed: toBeMappedCreateDto.Processed,
                tries: 0
            );
            var dbToBeMapped = await _toBeMappedRepository.FindByAlias(toBeMapped.Alias);

            if (dbToBeMapped == null)
            {
                var createdToBeMapped = await _toBeMappedRepository.InsertAsync(toBeMapped, true);
                return createdToBeMapped;
            }

            return null;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task UpdateASync(ToBeMappedDto toBeMapped)
    {
        var myToBeMapped = await  _toBeMappedRepository.FindByAlias(toBeMapped.Alias);
    }
}
