import { mapEnumToOptions } from '@abp/ng.core';

export enum FromService {
  FileService = 0,
  EmbyService = 1,
  DelugeService = 2,
  VideoService = 3,
}

export const fromServiceOptions = mapEnumToOptions(FromService);
