using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.TraktService.Lib.TraktShowNs.Dtos;
using MediaInAction.TraktService.TraktEpisodeNs;
using Microsoft.Extensions.Logging;

namespace MediaInAction.TraktService.Lib.TraktEpisodeNs;

public class TraktEpisodeLibService : ITraktEpisodeLibService
{
    private readonly ILogger<TraktEpisodeLibService> _logger;
    private readonly TraktEpisodeManager _traktEpisodeManager;
    private readonly ITraktEpisodeRepository _traktEpisodeRepository;

   
    public TraktEpisodeLibService(
        ILogger<TraktEpisodeLibService> logger,
        TraktEpisodeManager traktEpisodeManager,
        ITraktEpisodeRepository traktEpisodeRepository)
    {
        _traktEpisodeManager = traktEpisodeManager;
        _traktEpisodeRepository = traktEpisodeRepository;
        _logger = logger;
    }

    public async Task UpdateAddFromDto(CollectionEpisodeDto episodeDto)
    {
        _logger.LogInformation("TraktEpisodeLibService:UpdateAddFromDto" + episodeDto.ShowSlug + ":" +
                               episodeDto.EpisodeNum);
        try
        {
            await CreateUpdateEpisode(episodeDto);
        }
        catch (Exception ex)
        {
            _logger.LogDebug("TraktEpisodeLibService:UpdateAddFromDto" + ex.Message);
        }
    }

    public async Task<List<TraktEpisodeDto>> GetEpisodes()
    {
        var episodeList = await _traktEpisodeRepository.GetListAsync();
        var episodeListDto = new List<TraktEpisodeDto>();
        foreach (var episode in episodeList)
        {
            var episodeDto = new TraktEpisodeDto();
            episodeDto.EpisodeNum = episode.EpisodeNum;
            episodeDto.SeasonNum = episode.SeasonNum;
            episodeDto.ShowSlug = episode.ShowSlug;
            episodeDto.AiredDate = episode.AiredDate;
            episodeListDto.Add(episodeDto);
        }

        return episodeListDto;
    }

    public async Task CreateUpdateEpisode(CollectionEpisodeDto episodeDto)
    {
        var dbEpisode = await _traktEpisodeRepository.FindByTraktShowSlugSeasonEpisodeAsync(
            episodeDto.ShowSlug,
            episodeDto.SeasonNum,
            episodeDto.EpisodeNum);
        if (dbEpisode == null)
        {
            var createEpiodeDto = ConvertToCrateEpisodeDto(episodeDto);
            var createdEpisode = await _traktEpisodeManager.CreateAsync(createEpiodeDto);
        }
        else
        {
            var returnId = Guid.Empty;
            var diff = CompareTraktEpisode(dbEpisode, episodeDto);

            if (diff == true)
            {
                returnId = await UpdateTrakEpisode(dbEpisode, episodeDto);
            }
            else
            {
                returnId = Guid.Empty;
            }
        }
    }

    public async Task<List<TraktEpisodeDto>> GetListAsync()
    {
        var traktEpisodes = await _traktEpisodeRepository.GetListAsync();
        var traktEpisodeDtos = MapToDtos(traktEpisodes);
        return traktEpisodeDtos;
    }

    public async Task<List<TraktEpisodeDto>> GetEpisodeByShow(string slug)
    {
        try
        {
            var traktEpisodes = await _traktEpisodeRepository.GetEpisodesByShow(slug);
            if ((traktEpisodes != null) && (traktEpisodes.Count > 0))
            {
                var traktEpisodeDtos = MapToDtos(traktEpisodes);
                return traktEpisodeDtos;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            _logger.LogDebug("TraktEpisodeLibService:GetEpisodeByShow" + ex.Message);
            return null;
        }
    }

    public async Task ResendUnAcceptedEpisodesList()
    {
        var episodes = await _traktEpisodeRepository.GetChangedListAsync();
        foreach (var episode in episodes)
        {
            if (episode.Changed == true)
            {
                var episodeDto = MapToDto(episode);
            }
        }
    }

    private TraktEpisodeCreateDto ConvertToCrateEpisodeDto(CollectionEpisodeDto episodeDto)
    {
        var createEpisodeAliasList = new List<TraktEpisodeAliasCreateDto>();
        var traktId = "";
        foreach (var episodeAlias in episodeDto.CollectionEpisodeAliasDtos)
        {
            createEpisodeAliasList.Add(new TraktEpisodeAliasCreateDto
            {
                IdType = episodeAlias.IdType,
                IdValue = episodeAlias.IdValue
            });
            if (episodeAlias.IdType == "trakt")
            {
                traktId = episodeAlias.IdValue;
            }
        }

        var createEpisode = new TraktEpisodeCreateDto
        {
            ShowSlug = episodeDto.ShowSlug,
            SeasonNum = episodeDto.SeasonNum,
            EpisodeNum = episodeDto.EpisodeNum,
            EpisodeName = episodeDto.EpisodeName,
            AiredDate = episodeDto.AiredDate,
     
            TraktEpisodeCreateAliases = createEpisodeAliasList
        };

        return createEpisode;
    }

    private async Task<Guid> UpdateTrakEpisode(TraktEpisode dbEpisode,
        CollectionEpisodeDto episodeDto)
    {
        var updatedShow = dbEpisode;
        updatedShow.ShowSlug = episodeDto.ShowSlug;
        updatedShow.SeasonNum = episodeDto.SeasonNum;
        updatedShow.EpisodeNum = episodeDto.EpisodeNum;
        updatedShow.AiredDate = episodeDto.AiredDate;
        updatedShow.EpisodeName = episodeDto.EpisodeName;

        foreach (var alias in episodeDto.CollectionEpisodeAliasDtos)
        {
            var found = false;
            foreach (var dbAlias in dbEpisode.TraktEpisodeAliases)
            {
                if ((dbAlias.IdType == alias.IdType) && (dbAlias.IdValue == alias.IdValue))
                {
                    found = true;
                }
            }

            if (found == false)
            {
                var newTraktEpisodeAliasCreateDto = new TraktEpisodeAliasCreateDto();
               // dbEpisode.TraktEpisodeAliases.Add(newTraktEpisodeAliasCreateDto);
                _logger.LogInformation("Alias Added:" + alias.IdType + ":" + alias.IdValue);
            }
        }

        var episode = await _traktEpisodeRepository.UpdateAsync(updatedShow, true);
        var updatedEpisode = MapToDto(episode);
        await SendEpisodeUpdateEventAsync(updatedEpisode);
        return dbEpisode.Id;
    }

    private async Task SendEpisodeUpdateEventAsync(TraktEpisodeDto updatedShow)
    {
        _logger.LogInformation("Sending TraktEpisodeUpdated Event: " +
                               updatedShow.ShowSlug + ":" + updatedShow.EpisodeNum);

    }
    
    private bool CompareTraktEpisode(TraktEpisode dbEpisode,
        CollectionEpisodeDto episodeDto)
    {
        bool diff = dbEpisode.TraktEpisodeAliases.Count != episodeDto.CollectionEpisodeAliasDtos.Count;

        if (dbEpisode.ShowSlug != episodeDto.ShowSlug)
        {
            diff = true;
        }

        if (dbEpisode.SeasonNum != episodeDto.SeasonNum)
        {
            diff = true;
        }

        if (dbEpisode.EpisodeNum != episodeDto.EpisodeNum)
        {
            diff = true;
        }

        if (dbEpisode.AiredDate != episodeDto.AiredDate)
        {
            diff = true;
        }

        if (dbEpisode.EpisodeName != episodeDto.EpisodeName)
        {
            diff = true;
        }

        foreach (var alias in episodeDto.CollectionEpisodeAliasDtos)
        {
            var found = false;
            foreach (var dbAlias in dbEpisode.TraktEpisodeAliases)
            {
                if (dbAlias.IdType == alias.IdType)
                {
                    found = true;
                }
            }

            if (found == false)
            {
                diff = true;
            }
        }

        var tcs = new TaskCompletionSource<bool>(diff);
        return diff;
    }


    private List<TraktEpisodeDto> MapToDtos(List<TraktEpisode> traktEpisodes)
    {
        var traktEpisodeDtos = new List<TraktEpisodeDto>();
        foreach (var traktEpisode in traktEpisodes)
        {
            traktEpisodeDtos.Add(MapToDto(traktEpisode));
        }

        return traktEpisodeDtos;
    }

    private TraktEpisodeDto MapToDto(TraktEpisode traktEpisode)
    {
        try
        {
            var traktEpisodeDto = new TraktEpisodeDto
            {
                Id = traktEpisode.Id,
                ShowSlug = traktEpisode.ShowSlug,
                SeasonNum = traktEpisode.SeasonNum,
                EpisodeNum = traktEpisode.EpisodeNum,
                AiredDate = traktEpisode.AiredDate,
                EpisodeName = traktEpisode.EpisodeName,
            };
            if (traktEpisodeDto.TraktEpisodeAliasDtos == null)
            {
                traktEpisodeDto.TraktEpisodeAliasDtos = new List<TraktEpisodeAliasDto>();
            }

            foreach (var episodeAlias in traktEpisode.TraktEpisodeAliases)
            {
                traktEpisodeDto.TraktEpisodeAliasDtos.Add(new TraktEpisodeAliasDto
                {
                    IdType = episodeAlias.IdType,
                    IdValue = episodeAlias.IdValue
                });
            }

            return traktEpisodeDto;
        }

        catch (Exception ex)
        {
            _logger.LogDebug("TraktEpisodeLibService:MapToDto" + ex.Message);
            return null;
        }
    }
}