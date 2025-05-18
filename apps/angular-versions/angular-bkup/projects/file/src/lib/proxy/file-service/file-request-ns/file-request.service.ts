import { RestService, Rest } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { FileRequestCompleteInputDto, FileRequestStartDto, FileRequestStartResultDto } from '../file-requests-ns/dtos/models';
import type { FileRequestCreateDto, FileRequestDto } from '../file-requests-ns/models';

@Injectable({
  providedIn: 'root',
})
export class FileRequestService {
  apiName = 'File';
  

  complete = (fileType: string, input: FileRequestCompleteInputDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, FileRequestDto>({
      method: 'POST',
      url: '/api/file/file-request/complete',
      params: { fileType },
      body: input,
    },
    { apiName: this.apiName,...config });
  

  create = (input: FileRequestCreateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, FileRequestDto>({
      method: 'POST',
      url: '/api/file/file-request',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  handleWebhook = (traktType: string, payload: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, boolean>({
      method: 'POST',
      url: '/api/file/file-request/handle-webhook',
      params: { traktType, payload },
    },
    { apiName: this.apiName,...config });
  

  start = (fileType: string, input: FileRequestStartDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, FileRequestStartResultDto>({
      method: 'POST',
      url: '/api/file/file-request/start',
      params: { fileType },
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
