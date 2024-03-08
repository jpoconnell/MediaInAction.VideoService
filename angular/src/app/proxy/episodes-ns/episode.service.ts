import { RestService, Rest } from '@abp/ng.core';
import type { ListResultDto, PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { DashboardDto, DashboardInput, EpisodeCreateDto, EpisodeDto, GetEpisodeInput, GetEpisodesInput, GetMyEpisodesInput } from '../episode-ns/dtos/models';

@Injectable({
  providedIn: 'root',
})
export class EpisodeService {
  apiName = 'Default';
  

  acceptTraktEpisode = (input: EpisodeCreateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, EpisodeDto>({
      method: 'POST',
      url: '/api/app/episode/accept-trakt-episode',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  create = (input: EpisodeCreateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, EpisodeDto>({
      method: 'POST',
      url: '/api/app/episode',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, EpisodeDto>({
      method: 'GET',
      url: `/api/app/episode/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getDashboard = (input: DashboardInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, DashboardDto>({
      method: 'GET',
      url: '/api/app/episode/dashboard',
      params: { filter: input.filter },
    },
    { apiName: this.apiName,...config });
  

  getEpisode = (input: GetEpisodeInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, EpisodeDto>({
      method: 'GET',
      url: '/api/app/episode/episode',
      params: { seriesId: input.seriesId, seasonNum: input.seasonNum, episodeNum: input.episodeNum },
    },
    { apiName: this.apiName,...config });
  

  getEpisodes = (input: GetEpisodesInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, EpisodeDto[]>({
      method: 'GET',
      url: '/api/app/episode/episodes',
      params: { filter: input.filter },
    },
    { apiName: this.apiName,...config });
  

  getEpisodesBySpec = (input: GetEpisodesInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, EpisodeDto[]>({
      method: 'GET',
      url: '/api/app/episode/episodes-by-spec',
      params: { filter: input.filter },
    },
    { apiName: this.apiName,...config });
  

  getList = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, ListResultDto<EpisodeDto>>({
      method: 'GET',
      url: '/api/app/episode',
    },
    { apiName: this.apiName,...config });
  

  getListPaged = (input: PagedAndSortedResultRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<EpisodeDto>>({
      method: 'GET',
      url: '/api/app/episode/paged',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getMyEpisodes = (input: GetMyEpisodesInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, EpisodeDto[]>({
      method: 'GET',
      url: '/api/app/episode/my-episodes',
      params: { filter: input.filter },
    },
    { apiName: this.apiName,...config });
  

  setAsComplete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: `/api/app/episode/${id}/set-as-complete`,
    },
    { apiName: this.apiName,...config });
  

  setAsWatched = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: `/api/app/episode/${id}/set-as-watched`,
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: EpisodeDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'PUT',
      url: `/api/app/episode/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
