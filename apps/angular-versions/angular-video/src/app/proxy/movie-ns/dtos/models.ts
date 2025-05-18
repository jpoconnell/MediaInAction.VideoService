import type { EntityDto } from '@abp/ng.core';
import type { MovieStatus } from '../movie-status.enum';

export interface DashboardDto extends EntityDto {
  movieStatusDto: MovieStatusDto[];
}

export interface DashboardInput {
  filter?: string;
}

export interface GetMoviesInput {
  filter?: string;
}

export interface MovieAliasDto extends EntityDto<string> {
  movieId?: string;
  idType?: string;
  idValue?: string;
}

export interface MovieDto extends EntityDto<string> {
  name?: string;
  firstAiredYear: number;
  movieStatus?: MovieStatus;
  isActive: boolean;
  movieAliasDtos: MovieAliasDto[];
  imageName?: string;
}

export interface MovieStatusDto extends EntityDto {
  countOfStatusMovie: number;
  movieStatus?: string;
}
