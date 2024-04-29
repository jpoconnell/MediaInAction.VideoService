using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.Enums;
using MediaInAction.VideoService.EpisodeAliasNs;
using MediaInAction.VideoService.EpisodesAliasNs;
using Microsoft.Extensions.Logging;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Distributed;

namespace MediaInAction.VideoService.EpisodeNs;

public class EpisodeManager : DomainService
{
    private readonly IEpisodeRepository _episodeRepository;
    private readonly IDistributedEventBus _eventBus;
    private readonly ILogger<EpisodeManager> _logger;
    
    
    public EpisodeManager(IEpisodeRepository episodeRepository,
        ILogger<EpisodeManager> logger,
        IDistributedEventBus eventBus)
    {
        _episodeRepository = episodeRepository;
        _logger = logger;
        _eventBus = eventBus;
    }

    public async Task<Episode> CreateAsync(
        Guid seriesId,
        int seasonNum,
        int episodeNum,
        List<EpisodeAlias>
            episodeAliases,
        DateTime airedDate,
        string source = "",
        string episodeName = null,
        string altEpisodeId = null,
        string seasonEpisode = null
    )
    {
        var addAiredDate = Convert.ToDateTime("1/1/2000");

        if (airedDate != null)
        {
            addAiredDate = airedDate;
        }
        // Create new episode
        Episode episode = new Episode(
            id: GuidGenerator.Create(),
            seriesId: seriesId,
            seasonNum: seasonNum,
            episodeNum: episodeNum,
            airedDate: addAiredDate,
            episodeName: episodeName,
            seasonEpisode: seasonEpisode);
        
        // Add new episode aliases
        if (episodeAliases == null )
        {
            episode.AddEpisodeAlias(
                id: GuidGenerator.Create(),
                episodeId: episode.Id,
                idType: "name",
                idValue: "name"
            );
        }
        else if (episodeAliases.Count == 0)
        {
            episode.AddEpisodeAlias(
                id: GuidGenerator.Create(),
                episodeId: episode.Id,
                idType: "creationDate",
                idValue: DateTime.Now.ToString()
            );  
        }
        else
        {
            foreach (var episodeAlias in episodeAliases)
            {
                episode.AddEpisodeAlias(
                    id: GuidGenerator.Create(),
                    episodeId: episode.Id,
                    idType: episodeAlias.IdType,
                    idValue: episodeAlias.IdValue
                );
            }
        }

        try
        {
            var dbEpisode = await _episodeRepository.FindBySeriesIdSeasonEpisodeNum(seriesId,
                seasonNum, episodeNum,true);

            if (dbEpisode != null)
            {
                UpdateEpisode(dbEpisode,episode);
                return dbEpisode;
            }
            else {
                var createdEpisode = await _episodeRepository.InsertAsync(episode, true);
                var createdEpisodeAliases = MapAliases(createdEpisode.EpisodeAliases);
                // Create Episode event
                await _eventBus.PublishAsync(new EpisodeCreatedEto
                {
                    EpisodeId = createdEpisode.Id.ToString(),
                    SeriesId = createdEpisode.SeriesId.ToString(),
                    SeasonNum = createdEpisode.SeasonNum,
                    EpisodeNum = createdEpisode.EpisodeNum,
                    EpisodeAliases = createdEpisodeAliases
                });

                return createdEpisode;
            }
        }
        catch (Exception ex)
        {
            _logger.LogDebug(ex.Message);
            return null;
        }
        
    }
        
    private Episode UpdateEpisode(Episode dbEpisode, Episode episode)
    {
        var updated = 0; 
        if (dbEpisode.AiredDate != episode.AiredDate)
        {
            if (episode.AiredDate > Convert.ToDateTime("01/01/2020"))
            {
                dbEpisode.AiredDate = episode.AiredDate;
                updated++;
            }
        }

        foreach (var episodeAlias in episode.EpisodeAliases)
        {
            var found = false;
            foreach (var dbEpisodeAlias in dbEpisode.EpisodeAliases)
            {
                if ((dbEpisodeAlias.IdType == episodeAlias.IdType) &&
                    (dbEpisodeAlias.IdValue == episodeAlias.IdValue))
                {
                    found = true;
                }
            }

            if (found == false)
            {
                if (episodeAlias.IdType != "tmdb")
                {
                    dbEpisode.EpisodeAliases.Add(episodeAlias);
                    updated++;
                }
            }
        }

        if (updated > 0)
        {
            _episodeRepository.UpdateAsync(dbEpisode);
        }

        return dbEpisode;
    }
    
    public async Task SetStatusAsync(Guid episodeId, MediaStatus status)
    {
       var episode = await _episodeRepository.GetAsync(episodeId);
       episode.SetEpisodeStatus(status);
       await _episodeRepository.UpdateAsync(episode, true);
    }

    private List<EpisodeAliasCreatedEto> MapAliases(List<EpisodeAlias> aliases)
    {
        var episodeAliasList = new List<EpisodeAliasCreatedEto>();
        foreach (var alias in aliases)
        {
            if ((alias.IdType.Length > 0) && (alias.IdValue.Length > 0))
            {
                var newEpisodeAlias = new EpisodeAliasCreatedEto();
                newEpisodeAlias.IdType = alias.IdType;
                newEpisodeAlias.IdValue = alias.IdValue;
                episodeAliasList.Add(newEpisodeAlias);
            }
            else
            {
                _logger.LogInformation("Bad IdType or IdValue");
            }
        }
        return episodeAliasList;
    }
    
    /*
    public async Task<Episode> AcceptTraktEpisodeAsync(TraktService.TraktEpisodeNs.TraktEpisodeCreatedEto input, Guid seriesId )
    {
        _logger.LogInformation("AcceptTraktEpisodeAsync");
      
        var dbEpisode = await _episodeRepository.FindBySeriesIdSeasonEpisodeNum(
            seriesId, input.SeasonNum, input.EpisodeNum);
        
        if (dbEpisode == null)
        {
            var episodeAliases = MapAliases(input.TraktEpisodeCreatedAliases);
            var newEpisode = await CreateAsync(
                seriesId, 
                input.SeasonNum, 
                input.EpisodeNum,
                episodeAliases, 
                input.AiredDate, 
                input.EpisodeName,
                "trakt");
            return newEpisode;
        }
        else  // is an update
        {
            var slugName =   ":S" + input.SeasonNum.ToString() + "E" + input.EpisodeNum.ToString();
            _logger.LogInformation("Episode with Series,Season,Episode:" + slugName  + " found");
            try
            {
                
                _logger.LogInformation("Update Try");
                
                var updatedEpisode =  await _episodeRepository.UpdateAsync(dbEpisode, autoSave: true);
                return updatedEpisode;
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex.Message);
                return null;
            }
        }
    }


    public async Task<Episode> AcceptEmbyEpisodeAsync(EmbyEpisodeCreatedEto input, Guid seriesId)
    {
        _logger.LogInformation("AcceptEmbyEpisodeAsync");
        
        var episode = await _episodeRepository.FindBySeriesIdSeasonEpisodeNum(
            seriesId, input.SeasonNum, input.EpisodeNum);

        var aliases = MapAliases(input.Aliases);
        if (episode == null)
        {
            var newEpisode = await CreateAsync(
                seriesId, 
                input.SeasonNum, 
                input.EpisodeNum,
                aliases, 
                input.AiredDate, 
                input.EpisodeName );
            return newEpisode;
        }
        else  // is an update
        {
            var slugName =   ":S" +  input.SeasonNum.ToString() + "E" + input.EpisodeNum.ToString();
            _logger.LogInformation("Episode with Series,Season,Episode:" + slugName  + " found");
            try
            {
                _logger.LogInformation("Update Try");
                
                var id =  await _episodeRepository.UpdateAsync(episode, autoSave: true);
                return id;
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex.Message);
                return null;
            }
        }
    }

    private List<EpisodeAlias> MapAliases(List<TraktEpisodeAliasCreatedEto> aliases)
    {
        var episodeAliasList = new List<EpisodeAlias>();
        foreach (var alias in aliases)
        {
            if ((alias.IdType.Length > 0) && (alias.IdValue.Length > 0))
            {
                var newEpisodeAlias = new EpisodeAlias(alias.IdType,alias.IdValue);
                episodeAliasList.Add(newEpisodeAlias);
            }
            else
            {
                _logger.LogInformation("Bad IdType or IdValue");
            }
        }
        return episodeAliasList;
    }
  
    private List<EpisodeAlias> MapAliases(List<EmbyEpisodeAliasCreatedEto> aliases)
    {
        var episodeAliasList = new List<EpisodeAlias>();
        foreach (var alias in aliases)
        {
            if ((alias.IdType.Length > 0) && (alias.IdValue.Length > 0))
            {
                var newEpisodeAlias = new EpisodeAlias(alias.IdType,alias.IdValue);
                episodeAliasList.Add(newEpisodeAlias);
            }
            else
            {
                _logger.LogInformation("Bad IdType or IdValue");
            }
        }
        
        return episodeAliasList;
    }
    

    public async Task<Guid> AcceptTraktEpisodeAsync(
        TraktEpisodeAcknowledgeEto eventData, 
        Guid seriesId)
    {
        _logger.LogInformation("Acknowledge EmbyEpisodeAsync");
        
        var episode = await _episodeRepository.FindBySeriesIdSeasonEpisodeNum(
            seriesId, eventData.Season, eventData.Episode);
        
        if (episode != null)
        {
            var slugName =   ":S" +  eventData.Season.ToString() + "E" + eventData.Episode.ToString();
            _logger.LogInformation("Episode with Series,Season,Episode:" + slugName  + " found");
            try
            {
                episode.EventStatus = FileStatus.Accepted;
                var updatedEpisode =  await _episodeRepository.UpdateAsync(episode, autoSave: true);
                return updatedEpisode.Id;
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex.Message);
                return Guid.Empty;
            }
        }
        else
        {
            _logger.LogDebug("Should be an episode for this");
            return Guid.Empty;
        }
    }
    */
}
