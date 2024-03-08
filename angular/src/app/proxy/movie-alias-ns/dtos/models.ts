import type { EntityDto } from '@abp/ng.core';

export interface MovieAliasCreateDto {
  movieId?: string;
  idType?: string;
  idValue?: string;
}

export interface MovieAliasDto extends EntityDto<string> {
  movieId?: string;
  idType?: string;
  idValue?: string;
}
