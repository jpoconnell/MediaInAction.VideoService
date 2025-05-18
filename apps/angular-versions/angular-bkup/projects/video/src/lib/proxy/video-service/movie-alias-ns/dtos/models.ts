import type { EntityDto } from '@abp/ng.core';

export interface MovieAliasDto extends EntityDto<string> {
  movieId?: string;
  idType?: string;
  idValue?: string;
}
