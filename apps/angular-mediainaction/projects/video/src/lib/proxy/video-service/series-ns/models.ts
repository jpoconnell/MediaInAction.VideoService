import type { EntityDto } from '@abp/ng.core';
import type { SeriesStatus } from './series-status.enum';

export interface DashboardInput {
  filter?: string;
}

export interface GetMySeriessInput {
  filter?: string;
}

export interface GetSeriessInput {
  filter?: string;
}

export interface SeriesAliasCreateDto {
  seriesId?: string;
  idType?: string;
  idValue?: string;
}

export interface SeriesAliasDto extends EntityDto<string> {
  seriesId?: string;
  idType?: string;
  idValue?: string;
}

export interface SeriesCreateDto {
  name?: string;
  firstAiredYear: number;
  seriesStatus?: SeriesStatus;
  imageName?: string;
  seriesAliasCreateDtos: SeriesAliasCreateDto[];
}

export interface SeriesDashboardDto extends EntityDto {
  seriesStatusDto: SeriesStatusDto[];
  seriesIsActiveDto: SeriesIsActiveDto[];
}

export interface SeriesDto extends EntityDto<string> {
  seriesName?: string;
  firstAiredYear: number;
  seriesStatus?: SeriesStatus;
  imageName?: string;
  seriesAliasDtos: SeriesAliasDto[];
}

export interface SeriesIsActiveDto extends EntityDto {
  countOfInActiveSeries: number;
  seriesIsActive?: string;
}

export interface SeriesStatusDto extends EntityDto {
  countOfStatusSeries: number;
  seriesStatus?: string;
}
