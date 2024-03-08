import type { EntityDto } from '@abp/ng.core';
import type { MediaType } from '../../enums/media-type.enum';
import type { SeriesAliasCreateDto, SeriesAliasDto } from '../../series-alias-ns/dtos/models';

export interface ActiveDto extends EntityDto {
  seriesName?: string;
  pictureUrl?: string;
  episodes: number;
}

export interface DashboardDto extends EntityDto {
  actives: ActiveDto[];
  seriesStatusDto: SeriesStatusDto[];
}

export interface DashboardInput {
  filter?: string;
}

export interface GetSeriesInput {
  filter?: string;
}

export interface GetSeriessInput {
  filter?: string;
}

export interface SeriesCreateDto {
  name?: string;
  slug?: string;
  firstAiredYear: number;
  type: MediaType;
  isActive: boolean;
  seriesAliases: SeriesAliasCreateDto[];
}

export interface SeriesDto extends EntityDto<string> {
  name?: string;
  firstAiredYear: number;
  type: MediaType;
  isActive: boolean;
  imageName?: string;
  seriesAliasDtos: SeriesAliasDto[];
}

export interface SeriesStatusDto extends EntityDto {
  countOfStatusSeries: number;
  isActive?: string;
}
