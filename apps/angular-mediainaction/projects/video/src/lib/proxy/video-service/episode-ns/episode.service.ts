import type { DashboardDto, EpisodeDashboardInput, EpisodeDto, GetEpisodeInput, GetEpisodesInput, GetMyEpisodesInput } from './dtos/models';
import type { EpisodeCreateDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { ListResultDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class EpisodeService {
  apiName = 'Video';
  

  create = (input: EpisodeCreateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, EpisodeDto>({
      method: 'POST',
      url: '/api/video/episode',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, EpisodeDto>({
      method: 'GET',
      url: `/api/video/episode/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getDashboard = (input: EpisodeDashboardInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, DashboardDto>({
      method: 'GET',
      url: '/api/video/episode/dashboard',
      params: { filter: input.filter },
    },
    { apiName: this.apiName,...config });
  

  getEpisode = (input: GetEpisodeInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, EpisodeDto>({
      method: 'GET',
      url: '/api/video/episode/episode',
      params: { seriesId: input.seriesId, seasonNum: input.seasonNum, episodeNum: input.episodeNum },
    },
    { apiName: this.apiName,...config });
  

  getEpisodes = (input: GetEpisodesInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, EpisodeDto[]>({
      method: 'GET',
      url: '/api/video/episode/episodes',
      params: { filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getEpisodesBySpec = (input: GetEpisodesInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, EpisodeDto[]>({
      method: 'GET',
      url: '/api/video/episode/episodes-by-spec',
      params: { filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getList = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, ListResultDto<EpisodeDto>>({
      method: 'GET',
      url: '/api/video/episode',
    },
    { apiName: this.apiName,...config });
  

  getListPaged = (input: GetEpisodesInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<EpisodeDto>>({
      method: 'GET',
      url: '/api/video/episode/paged',
      params: { filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getMyEpisodes = (input: GetMyEpisodesInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, EpisodeDto[]>({
      method: 'GET',
      url: '/api/video/episode/my-episodes',
      params: { filter: input.filter },
    },
    { apiName: this.apiName,...config });
  

  setAsComplete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: `/api/video/episode/${id}/set-as-complete`,
    },
    { apiName: this.apiName,...config });
  

  setAsWatched = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: `/api/video/episode/${id}/set-as-watched`,
    },
    { apiName: this.apiName,...config });
  

  update = (input: EpisodeDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'PUT',
      url: '/api/video/episode',
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
