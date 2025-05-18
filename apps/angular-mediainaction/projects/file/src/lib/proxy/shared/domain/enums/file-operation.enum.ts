import { mapEnumToOptions } from '@abp/ng.core';

export enum FileOperation {
  UnCompress = 0,
  Move = 1,
  Delete = 2,
}

export const fileOperationOptions = mapEnumToOptions(FileOperation);
