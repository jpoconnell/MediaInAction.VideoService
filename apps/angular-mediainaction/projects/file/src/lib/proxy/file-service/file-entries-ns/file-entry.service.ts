import type { FileEntryCreateDto, FileEntryDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { ListResultDto, PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class FileEntryService {
  apiName = 'File';
  

  create = (input: FileEntryCreateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, FileEntryDto>({
      method: 'POST',
      url: '/api/file/file-entry',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/file/file-entry/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, FileEntryDto>({
      method: 'GET',
      url: `/api/file/file-entry/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, ListResultDto<FileEntryDto>>({
      method: 'GET',
      url: '/api/file/file-entry',
    },
    { apiName: this.apiName,...config });
  

  getListPaged = (input: PagedAndSortedResultRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<FileEntryDto>>({
      method: 'GET',
      url: '/api/file/file-entry/paged',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: FileEntryDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, FileEntryDto>({
      method: 'PUT',
      url: `/api/file/file-entry/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
