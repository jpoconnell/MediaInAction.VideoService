using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaInAction.TraktService.Permissions;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Specifications;
using MediaInAction.TraktService.TraktEpisodeNs.Dtos;
using MediaInAction.TraktService.TraktEpisodeNs.Specifications;

namespace MediaInAction.TraktService.TraktEpisodeNs;

[Authorize(TraktServicePermissions.TraktEpisode.Default)]
public class TraktEpisodeAppService : TraktServiceAppService, ITraktEpisodeAppService
{
    private readonly ITraktEpisodeRepository _traktEpisodeRepository;
    private readonly TraktEpisodeManager _traktEpisodeManager;
    private readonly ILogger<TraktEpisodeAppService> _logger;
    
    public TraktEpisodeAppService(
        ITraktEpisodeRepository traktEpisodeRepository,
        TraktEpisodeManager traktEpisodeManager,
        ILogger<TraktEpisodeAppService>   logger)
    {
        _traktEpisodeRepository = traktEpisodeRepository;
        _traktEpisodeManager = traktEpisodeManager;
        _logger = logger;
    }
    
    [Authorize(TraktServicePermissions.TraktEpisode.Dashboard)]
    public async Task<EpisodeDashboardDto> GetDashboardAsync(EpisodeDashboardInput input)
    {
        return new EpisodeDashboardDto()
        {
            TraktEpisodeStatusDto = await GetCountOfTotalEpisodeStatusAsync(input.Filter),
        };
    }
    
    [Authorize(TraktServicePermissions.TraktEpisode.Create)]
    public async Task<TraktEpisodeDto> CreateAsync(TraktEpisodeCreateDto input)
    {
        var episode = await _traktEpisodeManager.CreateAsync(input);
        if (episode == null)
        {
            return null;
        }
        else
        {
            var traktEpisodeDto = MapToDto(episode);
            return traktEpisodeDto;
        }
    }
    
    [Authorize(TraktServicePermissions.TraktEpisode.Update)]
    public async Task<TraktEpisodeDto> UpdateAsync(TraktEpisodeDto updatedShow, bool b)
    {
        var episode = await _traktEpisodeRepository.GetAsync(updatedShow.Id);
        episode.ExternalId = updatedShow.ExternalId;

        await _traktEpisodeRepository.UpdateAsync(episode);
        return MapToDto(episode);
    }

    private TraktEpisodeDto MapToDto(TraktEpisode episode)
    {
        var traktEpisodeAliasList = new List<TraktEpisodeAliasDto>();
        foreach (var episodeAlias in episode.TraktEpisodeAliases)
        {
            var newTraktEpisodeAlias = new TraktEpisodeAliasDto();
            newTraktEpisodeAlias.IdType = episodeAlias.IdType;
            newTraktEpisodeAlias.IdValue = episodeAlias.IdValue;
            traktEpisodeAliasList.Add(newTraktEpisodeAlias);
        }
        var traktEpisodeDto = new TraktEpisodeDto();
        traktEpisodeDto.Id = episode.Id;
        traktEpisodeDto.SeriesId = episode.SeriesId;
        traktEpisodeDto.EpisodeNum = episode.EpisodeNum;
        traktEpisodeDto.AiredDate = episode.AiredDate;
        traktEpisodeDto.TraktEpisodeAliasDtos = traktEpisodeAliasList;

        return traktEpisodeDto;
    }

    private async Task<List<TraktEpisodeStatusDto>> GetCountOfTotalEpisodeStatusAsync(string filter)
    {
        ISpecification<TraktEpisode> specification = SpecificationFactory.Create(filter);
        var episodes = await _traktEpisodeRepository.GetDashboardAsync(specification);
        return CreateEpisodeStatusDtoMapping(episodes);
    }
    
    private List<TraktEpisodeStatusDto> CreateEpisodeStatusDtoMapping(List<TraktEpisode> episodes)
    {
        var episodeStatus = episodes
            .GroupBy(p => p.TraktStatus)
            .Select(p => new TraktEpisodeStatusDto { CountOfStatusEpisode = p.Count(), EpisodeStatus = p.Key.ToString() })
            .OrderBy(p => p.CountOfStatusEpisode)
            .ToList();
        
        return episodeStatus;
    }
}