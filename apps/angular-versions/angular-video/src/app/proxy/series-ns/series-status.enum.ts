import { mapEnumToOptions } from '@abp/ng.core';

export enum SeriesStatus {
  New = 0,
  Unknown = 1,
  InActive = 2,
}

export const seriesStatusOptions = mapEnumToOptions(SeriesStatus);
