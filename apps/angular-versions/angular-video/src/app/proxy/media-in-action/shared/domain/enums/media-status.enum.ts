import { mapEnumToOptions } from '@abp/ng.core';

export enum MediaStatus {
  New = 0,
  Indexed = 1,
  Torrent = 2,
  Compressed = 3,
  UnCompressed = 4,
  Move = 5,
  Complete = 6,
  Watched = 7,
}

export const mediaStatusOptions = mapEnumToOptions(MediaStatus);
