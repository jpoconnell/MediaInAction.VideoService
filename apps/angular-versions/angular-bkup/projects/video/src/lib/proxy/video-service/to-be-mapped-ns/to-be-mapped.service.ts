import type { GetToBeMappedInput, GetToBeMappedsInput } from './dtos/models';
import type { ToBeMappedCreateDto, ToBeMappedDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ToBeMappedService {
  apiName = 'Video';
  

  create = (input: ToBeMappedCreateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ToBeMappedDto>({
      method: 'POST',
      url: '/api/video/to-be-mapped',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ToBeMappedDto>({
      method: 'GET',
      url: `/api/video/to-be-mapped/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getListPaged = (input: GetToBeMappedsInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<ToBeMappedDto>>({
      method: 'GET',
      url: '/api/video/to-be-mapped/paged',
      params: { filter: input.filter, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getToBeMapped = (input: GetToBeMappedInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ToBeMappedDto>({
      method: 'GET',
      url: '/api/video/to-be-mapped/to-be-mapped',
      params: { alias: input.alias },
    },
    { apiName: this.apiName,...config });
  

  getToBeMappeds = (getToBeMappedInput: GetToBeMappedsInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ToBeMappedDto[]>({
      method: 'GET',
      url: '/api/video/to-be-mapped/to-be-mappeds',
      params: { filter: getToBeMappedInput.filter, sorting: getToBeMappedInput.sorting, skipCount: getToBeMappedInput.skipCount, maxResultCount: getToBeMappedInput.maxResultCount },
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
