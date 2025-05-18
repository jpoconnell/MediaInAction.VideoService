using System;
using System.Threading.Tasks;
using MediaInAction.VideoService.SeriesNs;
using Microsoft.Extensions.Logging;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Distributed;

namespace MediaInAction.VideoService.EpisodeNs;

public class EpisodeManager : DomainService
{
    private readonly IEpisodeRepository _episodeRepository;
    private readonly ILogger<EpisodeManager> _logger; 
    private readonly ISeriesRepository _seriesRepository;
    private readonly ISeriesAliasRepository _seriesAliasRepository;
    
    public EpisodeManager(IEpisodeRepository episodeRepository,
        ISeriesRepository seriesRepository,
        ISeriesAliasRepository  seriesAliasRepository,
        ILogger<EpisodeManager> logger)
    {
        _episodeRepository = episodeRepository;
        _seriesRepository = seriesRepository;
        _seriesAliasRepository = seriesAliasRepository;
        _logger = logger;
    }

    public async Task<Episode> CreateUpdateAsync(EpisodeCreateDto episodeCreateDto)
    {
        var addAiredDate = Convert.ToDateTime("1/1/2000");

        if (episodeCreateDto.AiredDate == null)
        {
            episodeCreateDto.AiredDate = addAiredDate;
        }
        // Find series by name and year
        var dbSeries = new Series();
        if ((episodeCreateDto.SeriesId != null) && (episodeCreateDto.SeriesId != Guid.Empty))
        {
             dbSeries = await _seriesRepository.GetAsync((Guid)episodeCreateDto.SeriesId);
        }
        else if (episodeCreateDto.SeriesName != null)
        {
            var seriesYear = Convert.ToInt32(episodeCreateDto.SeriesYear);
            dbSeries = await _seriesRepository.FindBySeriesNameYear(episodeCreateDto.SeriesName, seriesYear);
        }
        else if (episodeCreateDto.Slug != null)
        {
            var dbSeriesAliasList = await _seriesAliasRepository.GetByIdValue(episodeCreateDto.Slug);

            if (dbSeriesAliasList == null)
            {
                _logger.LogError("Series does not exist for episode");
                return null;
            }
            else
            {
                if (dbSeriesAliasList.Count > 0)
                {
                    var dbSeriesAlias = dbSeriesAliasList[0];
                    if (dbSeriesAlias != null)
                    {
                        dbSeries = await _seriesRepository.GetByIdAsync(dbSeriesAlias.SeriesId);
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        if (dbSeries == null)
        { 
            _logger.LogInformation("Series not found");
            return null;
        }
        else
        {
            // Create new episode
            var episode = new Episode(
                id: GuidGenerator.Create(),
                seriesId: dbSeries.Id,
                seasonNum: episodeCreateDto.SeasonNum,
                episodeNum: episodeCreateDto.EpisodeNum,
                airedDate: (DateTime)episodeCreateDto.AiredDate,
                episodeName: episodeCreateDto.EpisodeName,
                seasonEpisode: ""
            );
                
            // Add new episode aliases
            if (episodeCreateDto.EpisodeCreateAliases == null)
            {
                episode.AddEpisodeAlias(
                    id: GuidGenerator.Create(),
                    episodeId: episode.Id,
                    idType: "name",
                    idValue: "name"
                );
            }
            else if (episodeCreateDto.EpisodeCreateAliases.Count == 0)
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
                foreach (var episodeAlias in episodeCreateDto.EpisodeCreateAliases)
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
                var dbEpisode = await _episodeRepository.FindBySeriesIdSeasonEpisodeNum(dbSeries.Id,
                    episode.SeasonNum, episode.EpisodeNum, true);

                if (dbEpisode != null)
                {
                    UpdateEpisode(dbEpisode, episode);
                    return dbEpisode;
                }
                else
                {
                    var createdEpisode = await _episodeRepository.InsertAsync(episode, true);
                    return createdEpisode;
                }
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex.Message);
            }
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

        if (dbEpisode.EpisodeName != episode.EpisodeName)
        {
            dbEpisode.EpisodeName = episode.EpisodeName;
            updated++;
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
    
    public async Task SetStatusAsync(Guid episodeId, EpisodeStatus status)
    {
       var episode = await _episodeRepository.GetAsync(episodeId);
       episode.SetEpisodeStatus(status);
       await _episodeRepository.UpdateAsync(episode, true);
    }
}
