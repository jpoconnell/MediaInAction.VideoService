import { RestService, Rest } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { DashboardDto, DashboardInput, FileEntryCreatedDto, FileEntryDto, GetFileEntryInput } from '../file-entry-ns/dtos/models';

@Injectable({
  providedIn: 'root',
})
export class FileEntryService {
  apiName = 'Default';
  

  create = (input: FileEntryCreatedDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, FileEntryDto>({
      method: 'POST',
      url: '/api/app/file-entry',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, FileEntryDto>({
      method: 'GET',
      url: `/api/app/file-entry/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getDashboard = (input: DashboardInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, DashboardDto>({
      method: 'GET',
      url: '/api/app/file-entry/dashboard',
      params: { filter: input.filter },
    },
    { apiName: this.apiName,...config });
  

  getFileEntry = (input: GetFileEntryInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, FileEntryDto>({
      method: 'GET',
      url: '/api/app/file-entry/file-entry',
      params: { server: input.server, directory: input.directory, fileName: input.fileName, listName: input.listName },
    },
    { apiName: this.apiName,...config });
  

  getListPaged = (input: PagedAndSortedResultRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<FileEntryDto>>({
      method: 'GET',
      url: '/api/app/file-entry/paged',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  setAsMapped = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: `/api/app/file-entry/${id}/set-as-mapped`,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
