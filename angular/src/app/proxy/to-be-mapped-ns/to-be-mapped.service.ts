import type { GetToBeMappedInput, GetToBeMappedsInput, ToBeMappedCreateDto, ToBeMappedDto } from './dtos/models';
import { RestService, Rest } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ToBeMappedService {
  apiName = 'Default';
  

  create = (input: ToBeMappedCreateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ToBeMappedDto>({
      method: 'POST',
      url: '/api/app/to-be-mapped',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ToBeMappedDto>({
      method: 'GET',
      url: `/api/app/to-be-mapped/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getToBeMapped = (input: GetToBeMappedInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ToBeMappedDto>({
      method: 'GET',
      url: '/api/app/to-be-mapped/to-be-mapped',
      params: { alias: input.alias },
    },
    { apiName: this.apiName,...config });
  

  getToBeMappeds = (getToBeMappedInput: GetToBeMappedsInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ToBeMappedDto[]>({
      method: 'GET',
      url: '/api/app/to-be-mapped/to-be-mappeds',
      params: { processed: getToBeMappedInput.processed },
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
