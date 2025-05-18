import type { EpisodeDto } from './dtos/models';
import { RestService, Rest } from '@abp/ng.core';
import type { ListResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class PublicEpisodeService {
  apiName = 'Video';
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, EpisodeDto>({
      method: 'GET',
      url: `/api/video/public-episode/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, ListResultDto<EpisodeDto>>({
      method: 'GET',
      url: '/api/video/public-episode',
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
