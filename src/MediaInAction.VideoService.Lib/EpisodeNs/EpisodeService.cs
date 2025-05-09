﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.Shared.Domain.Enums;
using MediaInAction.VideoService.EpisodeAliasNs;
using MediaInAction.VideoService.EpisodeNs.Dtos;
using MediaInAction.VideoService.EpisodeNs.Specifications;
using MediaInAction.VideoService.ParserNs;
using MediaInAction.VideoService.SeriesNs;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.Logging;
using Volo.Abp.Specifications;

namespace MediaInAction.VideoService.EpisodeNs;

public class EpisodeService: IEpisodeService
{
    private readonly IEpisodeRepository _episodeRepository;
    private readonly EpisodeManager _episodeManager;
    private readonly ISeriesService _seriesService;
    private readonly ILogger<EpisodeService> _logger;
    
    public EpisodeService(
        IEpisodeRepository episodeRepository,
        ISeriesService seriesService,
        ILogger<EpisodeService> logger,
        EpisodeManager episodeManager)
    {
        _episodeRepository = episodeRepository;
        _episodeManager = episodeManager;
        _seriesService = seriesService;
        _logger = logger;
    }

    public async Task<EpisodeDto> FindBySeriesIdSeasonEpisodeNum(string id, 
        int seasonNum, int episodeNum)
    {


        var myId = Guid.NewGuid();
        var episode = await _episodeRepository.FindBySeriesIdSeasonEpisodeNum(myId, seasonNum, episodeNum);
        if (episode != null)
        {
            return MapToEpisodeDto(episode);
        }
        else
        {
            return null;
        }
    }

    private EpisodeDto MapToEpisodeDto(Episode episode)
    {
        var episodeDto = new EpisodeDto
        {
            Id = episode.Id,
            EpisodeNum = episode.EpisodeNum,
            EpisodeStatus = episode.MediaStatus,
            SeasonNum = episode.SeasonNum,
            SeriesId = episode.SeriesId,
            AiredDate = episode.AiredDate,
            EpisodeName = episode.EpisodeName,
            SeasonEpisode = episode.SeasonEpisode
        };
        episodeDto.Id = episode.Id;
        return episodeDto;
    }

    public async Task<EpisodeDto> GetByIdAsync(Guid link)
    {
       var episode = await _episodeRepository.GetAsync(link);
       return MapToEpisodeDto(episode);
    }

    public async Task UpdateAsync(EpisodeDto episodeDto)
    {
        var episode = await _episodeRepository.FindBySeriesIdSeasonEpisodeNum(episodeDto.SeriesId,
            episodeDto.SeasonNum, episodeDto.EpisodeNum);

        if (episode.MediaStatus != episodeDto.EpisodeStatus)
        {
            episode.MediaStatus = episodeDto.EpisodeStatus;
            await _episodeRepository.UpdateAsync(episode, true);
        }
    }

    public async Task CreateAsync(ParserDto parser)
    {
        var newEpisodeAliases = new List<( string idType, string idValue)>();

        var newCreateEpisode = new EpisodeCreateDto
        {
            SeriesName = parser.SeriesName,
            // newCreateEpisode.SeriesYear = parser.S
            SeasonNum = parser.SeasonNum,
            EpisodeNum = parser.EpisodeNum
        };
        
        var newEpisode = await _episodeManager.CreateAsync(newCreateEpisode);
    }

    public async Task<List<EpisodeDto>> EpisodesNoDate(Guid seriesId)
    {
        var filter = "nd:" + seriesId.ToString(); // no date spec
        ISpecification<Episode> specification = SpecificationFactory.Create(filter);
        var episodeList = await _episodeRepository.GetMyListAsync(specification);

        var episodeDtoList = new List<EpisodeDto>();
        foreach (var episode in episodeList)
        {
            episodeDtoList.Add(MapToEpisodeDto(episode));
        }

        return episodeDtoList;
    }

    public async Task<List<EpisodeDto>> EpisodeFilesToMove()
    {
        try
        {
            var episodeDtoList = new List<EpisodeDto>();
            var status = (int) MediaStatus.UnCompressed;
            var filter = "st:" + status.ToString(); // episode status spec "UnCompressed"
            ISpecification<Episode> specification = SpecificationFactory.Create(filter);
            var episodeList = await _episodeRepository.GetMyListAsync(specification);

            if (episodeList.Count > 0)
            {
                foreach (var episode in episodeList)
                {
                    episodeDtoList.Add(MapToEpisodeDto(episode));
                }
                return episodeDtoList;
            }
            else
            {
                _logger.LogInformation("No Episodes to Move");
                return null;
            }
        }
        catch (Exception ex)
        {
            _logger.LogDebug("EpisodeFilesToMove" + ex.Message);
            return null;
        }
    }
    
    public async Task<List<EpisodeDto>> GetBySpec(ISpecification<Episode> specification)
    {
        var episodeDtoList = new List<EpisodeDto>();
        var episodeList = await _episodeRepository.GetMyListAsync(specification);

        foreach (var episode in episodeList)
        {
            episodeDtoList.Add(MapToEpisodeDto(episode));
        }

        return episodeDtoList;
    }
}

