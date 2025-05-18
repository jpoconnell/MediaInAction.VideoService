import type { DashboardDto, DashboardInput, GetMoviesInput, MovieDto } from './dtos/models';
import type { MovieCreateDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class MovieService {
  apiName = 'Video';
  

  create = (input: MovieCreateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, MovieDto>({
      method: 'POST',
      url: '/api/video/movie',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  exportMovieData = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: '/api/video/movie/export-movie-data',
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, MovieDto>({
      method: 'GET',
      url: `/api/video/movie/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getByMovieName = (name: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, MovieDto>({
      method: 'GET',
      url: '/api/video/movie/by-movie-name',
      params: { name },
    },
    { apiName: this.apiName,...config });
  

  getDashboard = (input: DashboardInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, DashboardDto>({
      method: 'GET',
      url: '/api/video/movie/dashboard',
      params: { filter: input.filter },
    },
    { apiName: this.apiName,...config });
  

  getListPaged = (input: GetMoviesInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<MovieDto>>({
      method: 'GET',
      url: '/api/video/movie/paged',
      params: { filter: input.filter },
    },
    { apiName: this.apiName,...config });
  

  getMovie = (newMovieName: string, newMovieFirstAiredYear: number, config?: Partial<Rest.Config>) =>
    this.restService.request<any, MovieDto>({
      method: 'GET',
      url: '/api/video/movie/movie',
      params: { newMovieName, newMovieFirstAiredYear },
    },
    { apiName: this.apiName,...config });
  

  getMovies = (input: GetMoviesInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, MovieDto[]>({
      method: 'GET',
      url: '/api/video/movie/movies',
      params: { filter: input.filter },
    },
    { apiName: this.apiName,...config });
  

  setAsInActive = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: `/api/video/movie/${id}/set-as-in-active`,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
