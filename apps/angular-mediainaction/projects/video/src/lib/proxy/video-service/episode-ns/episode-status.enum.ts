import { mapEnumToOptions } from '@abp/ng.core';

export enum EpisodeStatus {
  New = 0,
  Placed = 1,
  Watched = 2,
  Complete = 3,
}

export const episodeStatusOptions = mapEnumToOptions(EpisodeStatus);
