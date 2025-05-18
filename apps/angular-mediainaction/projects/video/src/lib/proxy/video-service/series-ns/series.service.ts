import type { DashboardInput, GetMySeriessInput, GetSeriessInput, SeriesCreateDto, SeriesDashboardDto, SeriesDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
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
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SeriesDto>({
      method: 'GET',
      url: `/api/video/series/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getByIdValueByInput = (input: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SeriesDto>({
      method: 'GET',
      url: '/api/video/series/by-id-value',
      params: { input },
    },
    { apiName: this.apiName,...config });
  

  getDashboard = (input: DashboardInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SeriesDashboardDto>({
      method: 'GET',
      url: '/api/video/series/dashboard',
      params: { filter: input.filter },
    },
    { apiName: this.apiName,...config });
  

  getListPaged = (input: PagedAndSortedResultRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<SeriesDto>>({
      method: 'GET',
      url: '/api/video/series/paged',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getMySeriess = (input: GetMySeriessInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SeriesDto[]>({
      method: 'GET',
      url: '/api/video/series/my-seriess',
      params: { filter: input.filter },
    },
    { apiName: this.apiName,...config });
  

  getSeriess = (input: GetSeriessInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SeriesDto[]>({
      method: 'GET',
      url: '/api/video/series/seriess',
      params: { filter: input.filter },
    },
    { apiName: this.apiName,...config });
  

  setAsInActive = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: `/api/video/series/${id}/set-as-in-active`,
    },
    { apiName: this.apiName,...config });
  

  update = (seriesDto: SeriesDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, SeriesDto>({
      method: 'PUT',
      url: '/api/video/series',
      body: seriesDto,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
