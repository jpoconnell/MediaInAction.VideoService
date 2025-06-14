﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.EpisodeNs.Dtos;
using MediaInAction.VideoService.EpisodeNs.Specifications;
using MediaInAction.VideoService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Specifications;
using Volo.Abp.Users;

namespace MediaInAction.VideoService.EpisodeNs
{
    public class EpisodeAppService : ApplicationService, IEpisodeAppService
    {
        private readonly IEpisodeRepository _episodeRepository;
        private readonly EpisodeManager _episodeManager;
        private readonly ILogger<EpisodeAppService> _logger;
        
        public EpisodeAppService(
            IEpisodeRepository episodeRepository,
            EpisodeManager episodeManager,
            ILogger<EpisodeAppService> logger)
        {
            _episodeRepository = episodeRepository;
            _episodeManager = episodeManager;
            _logger = logger;
        }

        [AllowAnonymous]
        public async Task<ListResultDto<EpisodeDto>> GetListAsync()
        {
            var episodeList = await _episodeRepository.GetListAsync(true);
            var episodeDtoList = new List<EpisodeDto>();
            foreach (var eps in episodeList)
            {
               var episodeDto = CreateEpisodeDtoMapping(eps);
               episodeDtoList.Add(episodeDto);
            }

            return new ListResultDto<EpisodeDto>(episodeDtoList);
        }

        public async Task<EpisodeDto> CreateAsync(EpisodeCreateDto input)
        {
            var newEpisode = await _episodeManager.CreateUpdateAsync(input);
            return CreateEpisodeDtoMapping(newEpisode);
        }
        
        public async Task<EpisodeDto> GetAsync(Guid id)
        {
            var episode = await _episodeRepository.GetAsync(id);
            return ObjectMapper.Map<Episode, EpisodeDto>(episode);
        }

        public async Task<EpisodeDto> GetEpisodeAsync(GetEpisodeInput input)
        {
            var episode = await _episodeRepository.FindBySeriesIdSeasonEpisodeNum(
                input.SeriesId,input.SeasonNum, input.EpisodeNum);
            return CreateEpisodeDtoMapping(episode);
        } 

        public async Task<List<EpisodeDto>> GetEpisodesBySpecAsync(GetEpisodesInput input)
        {
            if (CurrentUser.Id == null)
            {
                return new List<EpisodeDto>();
            }
    
            ISpecification<Episode> specification = SpecificationFactory.Create(input.Filter);
            var episodes = await _episodeRepository.GetEpisodesByUserId(CurrentUser.GetId(), specification, true);
            return CreateEpisodeDtoMapping(episodes);
        }

        public async Task<List<EpisodeDto>> GetEpisodesAsync(GetEpisodesInput input)
        {
            ISpecification<Episode> specification = SpecificationFactory.Create(input.Filter);
            var episodes = await _episodeRepository.GetMyListAsync(specification);
            return CreateEpisodeDtoMapping(episodes);
        }

        [Authorize(VideoServicePermissions.Episodes.Update)]
        public async Task UpdateAsync(EpisodeDto input)
        {
            var episodeList = await _episodeRepository.GetAsync(input.Id);
          
            //  mediaLocationAlias.SetFileName(input.FullPath);
        }
        
        [Authorize(VideoServicePermissions.Episodes.SetAsComplete)]
        public async Task SetAsCompleteAsync(Guid id)
        {
            await _episodeManager.SetStatusAsync(id, EpisodeStatus.Complete);
        }

        [Authorize(VideoServicePermissions.Episodes.SetAsWatched)]
        public async Task SetAsWatchedAsync(Guid id)
        {
            await _episodeManager.SetStatusAsync(id,EpisodeStatus.Watched);
        }

        public async Task<List<EpisodeDto>> GetMyEpisodesAsync(GetMyEpisodesInput input)
        {
            ISpecification<Episode> specification = SpecificationFactory.Create(input.Filter);
            var episodes = await _episodeRepository.GetEpisodesByUserId(CurrentUser.GetId(), specification, true);
            return CreateEpisodeDtoMapping(episodes);
        }

        public async Task<PagedResultDto<EpisodeDto>> GetListPagedAsync(GetEpisodesInput input)
        { 
            ISpecification<Episode> specification = SpecificationFactory.Create("a:");

            var episodeList =
                await _episodeRepository.GetListPagedAsync(specification, input.SkipCount,
                    input.MaxResultCount, "EpisodeName",true );

            var episodeDtoList = CreateEpisodeDtoMapping(episodeList);
            var totalCount = await _episodeRepository.GetCountAsync();
            return new PagedResultDto<EpisodeDto>(totalCount,episodeDtoList);
        }
        
        public async Task<DashboardDto> GetDashboardAsync(EpisodeDashboardInput input)
        {
            return new DashboardDto()
            {
                EpisodeStatusDto = await GetCountOfTotalEpisodeStatusAsync(input.Filter),
            };
        }

        private async Task<List<EpisodeDto>> GetCountOfTotalEpisodeStatusAsync(string inputFilter)
        {
            ISpecification<Episode> specification = SpecificationFactory.Create(inputFilter);
            var episodes = await _episodeRepository.GetDashboardAsync(specification);
            return CreateEpisodeDtoMapping(episodes);
        }

        private List<( string idType, string idValue
            )> GetEpisodeAliasTuple(List<EpisodeAliasCreateDto> inEpisodeAliases)
        {
            var seriesAliases =
                new List<(  string idType, string idValue)>();
            foreach (var seriesAlias in inEpisodeAliases)
            {
                seriesAliases.Add((  seriesAlias.IdType, seriesAlias.IdValue ));
            }
            return seriesAliases;
        }
        
        private List<EpisodeDto> CreateEpisodeDtoMapping(List<Episode> episodes)
        {
            List<EpisodeDto> dtoList = new List<EpisodeDto>();
            foreach (var episode in episodes)
            {
                dtoList.Add(CreateEpisodeDtoMapping(episode));
            }

            return dtoList;
        }

        private EpisodeDto CreateEpisodeDtoMapping(Episode episode)
        {
            if (episode != null)
            {
                return new EpisodeDto()
                {
                    Id = episode.Id,
                    SeriesId = episode.SeriesId,
                    SeasonNum = episode.SeasonNum,
                    EpisodeNum = episode.EpisodeNum,
                    EpisodeAliasDtos = new List<EpisodeAliasDto>()
                };
            }
            else
            {
                return null;
            }
        }
    }
}
