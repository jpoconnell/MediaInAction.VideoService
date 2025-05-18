import type { FromService } from '../media-in-action/shared/domain/enums/from-service.enum';
import type { MediaType } from '../media-in-action/shared/domain/enums/media-type.enum';
import type { EntityDto } from '@abp/ng.core';

export interface ToBeMappedCreateDto {
  alias?: string;
  processed: boolean;
  fromService?: FromService;
  fromId?: string;
  type?: MediaType;
  tries: number;
}

export interface ToBeMappedDto extends EntityDto {
  alias?: string;
  processed: boolean;
  fromService?: FromService;
  fromId?: string;
  type?: MediaType;
  tries: number;
}
