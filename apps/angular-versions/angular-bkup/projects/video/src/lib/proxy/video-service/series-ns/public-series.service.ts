import type { GetSeriesListInput } from './dtos/models';
import type { SeriesDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
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
    this.restService.request<any, SeriesDto[]>({
      method: 'GET',
      url: '/api/video/public-series',
    },
    { apiName: this.apiName,...config });
  

  getSeriesList = (filter: GetSeriesListInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SeriesDto[]>({
      method: 'GET',
      url: '/api/video/public-series/series-list',
      params: { filter: filter.filter, sorting: filter.sorting, skipCount: filter.skipCount, maxResultCount: filter.maxResultCount },
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
