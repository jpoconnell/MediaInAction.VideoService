import { mapEnumToOptions } from '@abp/ng.core';

export enum FileEntryStatus {
  New = 0,
  Created = 1,
  Mapped = 2,
}

export const fileEntryStatusOptions = mapEnumToOptions(FileEntryStatus);
