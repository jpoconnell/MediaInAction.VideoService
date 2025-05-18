import type { DashboardDto, DashboardInput, GetSeriesInput, GetSeriesListInput } from './dtos/models';
import type { SeriesCreateDto, SeriesDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class SeriesService {
  apiName = 'Video';
  

  create = (input: SeriesCreateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SeriesDto>({
      method: 'POST',
      url: '/api/video/series',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  exportSeriesData = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: '/api/video/series/export-series-data',
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SeriesDto>({
      method: 'GET',
      url: `/api/video/series/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getByIdValueBySlug = (slug: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SeriesDto>({
      method: 'GET',
      url: '/api/video/series/by-id-value',
      params: { slug },
    },
    { apiName: this.apiName,...config });
  

  getByNameYearBySeriesName = (seriesName: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SeriesDto>({
      method: 'GET',
      url: '/api/video/series/by-name-year',
      params: { seriesName },
    },
    { apiName: this.apiName,...config });
  

  getBySeriesName = (name: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SeriesDto>({
      method: 'GET',
      url: '/api/video/series/by-series-name',
      params: { name },
    },
    { apiName: this.apiName,...config });
  

  getSeries = (input: GetSeriesInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SeriesDto>({
      method: 'GET',
      url: '/api/video/series/series',
      params: { filter: input.filter },
    },
    { apiName: this.apiName,...config });
  

  getSeriesDashboard = (input: DashboardInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, DashboardDto>({
      method: 'GET',
      url: '/api/video/series/series-dashboard',
      params: { filter: input.filter },
    },
    { apiName: this.apiName,...config });
  

  getSeriesList = (input: GetSeriesListInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SeriesDto[]>({
      method: 'GET',
      url: '/api/video/series/series-list',
      params: { filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getSeriesListPaged = (input: GetSeriesListInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<SeriesDto>>({
      method: 'GET',
      url: '/api/video/series/series-list-paged',
      params: { filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  setAsInActive = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: `/api/video/series/${id}/set-as-in-active`,
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: SeriesDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SeriesDto>({
      method: 'PUT',
      url: `/api/video/series/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
