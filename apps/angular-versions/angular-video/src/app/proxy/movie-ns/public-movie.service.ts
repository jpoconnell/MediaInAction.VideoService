import type { MovieDto } from './dtos/models';
import { RestService, Rest } from '@abp/ng.core';
import type { ListResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class PublicMovieService {
  apiName = 'Video';
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, MovieDto>({
      method: 'GET',
      url: `/api/video/public-movie/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, ListResultDto<MovieDto>>({
      method: 'GET',
      url: '/api/video/public-movie',
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
