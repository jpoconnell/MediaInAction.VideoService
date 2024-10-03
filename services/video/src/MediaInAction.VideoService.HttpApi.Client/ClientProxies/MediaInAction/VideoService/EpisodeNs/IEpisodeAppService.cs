// This file is automatically generated by ABP framework to use MVC Controllers from CSharp
using MediaInAction.VideoService.EpisodeNs;
using MediaInAction.VideoService.EpisodeNs.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

// ReSharper disable once CheckNamespace
namespace MediaInAction.VideoService.EpisodeNs;

public interface IEpisodeAppService : IApplicationService
{
    Task<EpisodeDto> CreateAsync(EpisodeCreateDto input);

    Task<EpisodeDto> GetAsync(Guid id);

    Task<EpisodeDto> GetEpisodeAsync(GetEpisodeInput input);

    Task<List<EpisodeDto>> GetEpisodesAsync(GetEpisodesInput input);

    Task SetAsCompleteAsync(Guid id);

    Task SetAsWatchedAsync(Guid id);

    Task<PagedResultDto<EpisodeDto>> GetListPagedAsync(GetEpisodesInput input);

    Task<DashboardDto> GetDashboardAsync(DashboardInput input);

    Task<List<EpisodeDto>> GetMyEpisodesAsync(GetMyEpisodesInput getMyEpisodesInput);
}
