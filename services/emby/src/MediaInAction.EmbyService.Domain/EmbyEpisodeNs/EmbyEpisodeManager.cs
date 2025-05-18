using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.EmbyService.EmbyEpisodeAliasNs;
using Microsoft.Extensions.Logging;
using Volo.Abp.Domain.Services;

namespace MediaInAction.EmbyService.EmbyEpisodeNs;

public class EmbyEpisodeManager : DomainService
{
    private readonly IEmbyEpisodeRepository _embyEpisodeRepository;
    private ILogger<EmbyEpisodeManager> _logger;
    
    public EmbyEpisodeManager(
        IEmbyEpisodeRepository embyEpisodeRepository,
        ILogger<EmbyEpisodeManager> logger
    )
    {
        _embyEpisodeRepository = embyEpisodeRepository;
        _logger = logger;
    }

    public async Task<EmbyEpisode> CreateAsync(EmbyEpisodeCreateDto input)
    {
        try
        {
            // Create new episode
            var embyEpisodeId = GuidGenerator.Create();
            var embyEpisode = new EmbyEpisode
            {
                ShowId = input.EmbySeriesId,
                SeasonNum = input.SeasonNum,
                EpisodeNum = input.EpisodeNum,
                AiredDate = input.AiredDate,
                EpisodeAliases = new List<EmbyEpisodeAlias>()
            };

            foreach (var alias in input.EmbyEpisodeAliasCreateDto)
            {
                embyEpisode.EpisodeAliases.Add(new EmbyEpisodeAlias
                {
                    EpisodeId = embyEpisodeId,
                    IdType = alias.IdType,
                    IdValue = alias.IdValue
                });
            }
            var createdEpisode = await _embyEpisodeRepository.InsertAsync(embyEpisode,true);
            return createdEpisode;
        }
        catch (Exception ex)
        {
            _logger.LogDebug(ex.Message);
            return null;
        }
    }
}
