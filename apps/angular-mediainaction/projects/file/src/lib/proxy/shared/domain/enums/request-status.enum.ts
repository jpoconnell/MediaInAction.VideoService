import { mapEnumToOptions } from '@abp/ng.core';

export enum RequestStatus {
  New = 0,
  Complete = 1,
}

export const requestStatusOptions = mapEnumToOptions(RequestStatus);
