import { mapEnumToOptions } from '@abp/ng.core';

export enum ListType {
  Other = 0,
  Uncompressed = 1,
  Compressed = 2,
  Current = 3,
  Index = 4,
  Torrent = 5,
  Move = 6,
}

export const listTypeOptions = mapEnumToOptions(ListType);
