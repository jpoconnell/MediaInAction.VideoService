import { mapEnumToOptions } from '@abp/ng.core';

export enum MediaType {
  Other = 0,
  Episode = 1,
  Movie = 2,
  Training = 3,
  Music = 4,
  EBook = 5,
}

export const mediaTypeOptions = mapEnumToOptions(MediaType);
