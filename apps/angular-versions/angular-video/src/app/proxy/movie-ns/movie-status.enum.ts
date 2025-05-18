import { mapEnumToOptions } from '@abp/ng.core';

export enum MovieStatus {
  New = 0,
  InActive = 1,
  Active = 2,
}

export const movieStatusOptions = mapEnumToOptions(MovieStatus);
