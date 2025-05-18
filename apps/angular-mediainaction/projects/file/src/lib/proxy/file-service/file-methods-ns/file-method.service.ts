import type { FileMethod } from './dtos/models';
import { RestService, Rest } from '@abp/ng.core';
import { Injectable } from '@angular/core';

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
