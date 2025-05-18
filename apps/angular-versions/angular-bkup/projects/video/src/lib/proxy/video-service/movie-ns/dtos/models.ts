import type { EntityDto } from '@abp/ng.core';
import type { MediaType } from '../../../shared/domain/enums/media-type.enum';
import type { MediaStatus } from '../../../shared/domain/enums/media-status.enum';
import type { MovieAliasDto } from '../../movie-alias-ns/dtos/models';

export interface DashboardDto extends EntityDto {
  movieStatusDto: MovieStatusDto[];
}

export interface DashboardInput {
  filter?: string;
}

export interface GetMoviesInput {
  filter?: string;
}

export interface MovieDto extends EntityDto<string> {
  name?: string;
  firstAiredYear: number;
  type: MediaType;
  movieStatus: MediaStatus;
  isActive: boolean;
  movieAliasDtos: MovieAliasDto[];
}

export interface MovieStatusDto extends EntityDto {
  countOfStatusMovie: number;
  movieStatus?: string;
}
