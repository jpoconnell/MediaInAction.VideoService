import type { SeriesDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { ListResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class PublicSeriesService {
  apiName = 'Video';
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SeriesDto>({
      method: 'GET',
      url: `/api/video/public-series/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, ListResultDto<SeriesDto>>({
      method: 'GET',
      url: '/api/video/public-series',
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
