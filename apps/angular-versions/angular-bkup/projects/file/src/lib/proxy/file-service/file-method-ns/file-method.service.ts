import { RestService, Rest } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { FileMethod } from '../file-methods-ns/dtos/models';

@Injectable({
  providedIn: 'root',
})
export class FileMethodService {
  apiName = 'File';
  

  getList = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, FileMethod[]>({
      method: 'GET',
      url: '/api/file/file-method',
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
