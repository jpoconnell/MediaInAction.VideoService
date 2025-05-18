import type { EntityDto } from '@abp/ng.core';

export interface SeriesAliasCreateDto {
  idType?: string;
  idValue?: string;
}

export interface SeriesAliasDto extends EntityDto<string> {
  seriesId?: string;
  idType?: string;
  idValue?: string;
}
