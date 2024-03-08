import type { DashboardDto, DashboardInput, GetSeriesInput, GetSeriessInput, SeriesCreateDto, SeriesDto } from './dtos/models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class SeriesService {
  apiName = 'Default';
  

  create = (input: SeriesCreateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SeriesDto>({
      method: 'POST',
      url: '/api/app/series',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  exportInActive = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: '/api/app/series/export-in-active',
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SeriesDto>({
      method: 'GET',
      url: `/api/app/series/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getByIdValueBySlug = (slug: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SeriesDto>({
      method: 'GET',
      url: '/api/app/series/by-id-value',
      params: { slug },
    },
    { apiName: this.apiName,...config });
  

  getBySeriesName = (name: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SeriesDto>({
      method: 'GET',
      url: '/api/app/series/by-series-name',
      params: { name },
    },
    { apiName: this.apiName,...config });
  

  getDashboard = (input: DashboardInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, DashboardDto>({
      method: 'GET',
      url: '/api/app/series/dashboard',
      params: { filter: input.filter },
    },
    { apiName: this.apiName,...config });
  

  getListPaged = (input: PagedAndSortedResultRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<SeriesDto>>({
      method: 'GET',
      url: '/api/app/series/paged',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getSeries = (input: GetSeriesInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SeriesDto>({
      method: 'GET',
      url: '/api/app/series/series',
      params: { filter: input.filter },
    },
    { apiName: this.apiName,...config });
  

  getSeriess = (input: GetSeriessInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SeriesDto[]>({
      method: 'GET',
      url: '/api/app/series/seriess',
      params: { filter: input.filter },
    },
    { apiName: this.apiName,...config });
  

  importInActive = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: '/api/app/series/import-in-active',
    },
    { apiName: this.apiName,...config });
  

  setAsInActive = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: `/api/app/series/${id}/set-as-in-active`,
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: SeriesDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SeriesDto>({
      method: 'PUT',
      url: `/api/app/series/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
