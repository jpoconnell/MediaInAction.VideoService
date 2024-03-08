import type { DashboardDto, DashboardInput, GetMoviesInput, MovieCreateDto, MovieDto } from './dtos/models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedAndSortedResultRequestDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class MovieService {
  apiName = 'Default';
  

  create = (input: MovieCreateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, MovieDto>({
      method: 'POST',
      url: '/api/app/movie',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, MovieDto>({
      method: 'GET',
      url: `/api/app/movie/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getByMovieName = (name: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, MovieDto>({
      method: 'GET',
      url: '/api/app/movie/by-movie-name',
      params: { name },
    },
    { apiName: this.apiName,...config });
  

  getDashboard = (input: DashboardInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, DashboardDto>({
      method: 'GET',
      url: '/api/app/movie/dashboard',
      params: { filter: input.filter },
    },
    { apiName: this.apiName,...config });
  

  getListPaged = (input: PagedAndSortedResultRequestDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<MovieDto>>({
      method: 'GET',
      url: '/api/app/movie/paged',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getMovie = (newMovieName: string, newMovieFirstAiredYear: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, MovieDto>({
      method: 'GET',
      url: '/api/app/movie/movie',
      params: { newMovieName, newMovieFirstAiredYear },
    },
    { apiName: this.apiName,...config });
  

  getMovies = (input: GetMoviesInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, MovieDto[]>({
      method: 'GET',
      url: '/api/app/movie/movies',
      params: { filter: input.filter },
    },
    { apiName: this.apiName,...config });
  

  setAsInActive = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: `/api/app/movie/${id}/set-as-in-active`,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
