import type { EntityDto } from '@abp/ng.core';

export interface GetToBeMappedInput {
  alias?: string;
}

export interface GetToBeMappedsInput {
  processed: boolean;
}

export interface ToBeMappedCreateDto {
  alias?: string;
  processed: boolean;
}

export interface ToBeMappedDto extends EntityDto<string> {
  alias?: string;
  processed: boolean;
  tries: number;
}
