import type { SeriesAliasCreateDto, SeriesAliasDto } from '../series-alias-ns/models';
import type { EntityDto } from '@abp/ng.core';
import type { MediaType } from '../../shared/domain/enums/media-type.enum';


export interface SeriesCreateDto {
  name?: string;
  firstAiredYear: number;
  imageName?: string;
  seriesAliases: SeriesAliasCreateDto[];
}

export interface SeriesDto extends EntityDto<string> {
  name?: string;
  firstAiredYear: number;
  type: MediaType;
  isActive: boolean;
  imageName?: string;
  seriesAliasDtos: SeriesAliasDto[];
}
