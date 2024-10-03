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
    private readonly ILogger<TraktEpisodeAppService> _logger;
    private readonly ITraktEpisodeRepository _traktEpisodeRepository;
    private readonly TraktEpisodeManager _traktEpisodeManager;

    public TraktEpisodeAppService(
        ITraktEpisodeRepository traktEpisodeRepository,
        ILogger<TraktEpisodeAppService> logger,
        TraktEpisodeManager traktEpisodeManager)
    {
        _traktEpisodeRepository = traktEpisodeRepository;
        _traktEpisodeManager = traktEpisodeManager;
        _logger = logger;
    }
    
    [Authorize(TraktServicePermissions.TraktShow.Dashboard)]
    public async Task<EpisodeDashboardDto> GetDashboardAsync(EpisodeDashboardInput input)
    {
        return new EpisodeDashboardDto()
        {
            TraktEpisodeStatusDto = await GetCountOfTotalEpisodeStatusAsync(input.Filter),
        };
    }
    
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
                //ObjectMapper.Map<TraktEpisode, TraktEpisodeDto>(episode);
            return traktEpisodeDto;
        }
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
        traktEpisodeDto.ShowSlug = episode.ShowSlug;
        traktEpisodeDto.EpisodeNum = episode.EpisodeNum;
        traktEpisodeDto.AiredDate = episode.AiredDate;
        traktEpisodeDto.TraktEpisodeAliasDtos = traktEpisodeAliasList;

        return traktEpisodeDto;
    }

    private async Task<List<TraktEpisodeStatusDto>> GetCountOfTotalEpisodeStatusAsync(string filter)
    {
        ISpecification<TraktEpisode> specification = SpecificationFactory.Create(filter);
        var orders = await _traktEpisodeRepository.GetDashboardAsync(specification);
        return CreateEpisodeStatusDtoMapping(orders);
    }
    
    private List<TraktEpisodeStatusDto> CreateEpisodeStatusDtoMapping(List<TraktEpisode> episodes)
    {
        var episodeStatus = episodes
            .GroupBy(p => p.TraktStatus)
            .Select(p => new TraktEpisodeStatusDto { CountOfStatusEpisode = p.Count(), EpisodeStatus = p.Key.ToString() })
            .OrderBy(p => p.CountOfStatusEpisode)
            .ToList();
        
        episodeStatus.Add(new TraktEpisodeStatusDto() { EpisodeStatus = "test", CountOfStatusEpisode   = 3 });

        return episodeStatus;
    }
}