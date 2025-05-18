import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

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

export interface GetSeriesListInput extends PagedAndSortedResultRequestDto {
  filter?: string;
}

export interface SeriesStatusDto extends EntityDto {
  countOfStatusSeries: number;
  isActive?: string;
}
