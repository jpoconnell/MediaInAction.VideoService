import type { PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface GetToBeMappedInput {
  alias?: string;
}

export interface GetToBeMappedsInput extends PagedAndSortedResultRequestDto {
  filter: boolean;
}
