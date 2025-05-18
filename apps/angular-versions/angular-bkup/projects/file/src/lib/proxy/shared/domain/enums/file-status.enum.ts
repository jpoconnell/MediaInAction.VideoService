import { mapEnumToOptions } from '@abp/ng.core';

export enum FileStatus {
  New = 0,
  Accepted = 1,
  Mapped = 2,
  ToMove = 3,
  ToWatch = 4,
  ToDelete = 5,
}

export const fileStatusOptions = mapEnumToOptions(FileStatus);
