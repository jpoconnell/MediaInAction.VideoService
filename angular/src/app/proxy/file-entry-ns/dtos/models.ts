import type { EntityDto } from '@abp/ng.core';
import type { ListType } from '../../enums/list-type.enum';
import type { MediaType } from '../../enums/media-type.enum';
import type { FileStatus } from '../../enums/file-status.enum';
import type { FileEntryStatus } from '../file-entry-status.enum';

export interface DashboardDto extends EntityDto {
  fileEntryStatusDto: FileEntryStatusDto[];
}

export interface DashboardInput {
  filter?: string;
}

export interface FileEntryCreatedDto {
  fileId?: string;
  server?: string;
  directory?: string;
  fileName?: string;
  size: number;
  resolution?: string;
  extn?: string;
  listName: ListType;
  sequence: number;
  mediaType: MediaType;
  errorMessage?: string;
  fileStatus: FileStatus;
  fileEntryStatus: FileEntryStatus;
  updates: number;
}

export interface FileEntryDto extends EntityDto<string> {
  server?: string;
  directory?: string;
  fileName?: string;
  cleanFileName?: string;
  resolution?: string;
  extn?: string;
  size: number;
  link?: string;
  seriesLink?: string;
  listName: ListType;
  sequence: number;
  mediaType: MediaType;
  fileStatus: FileStatus;
  errorMessage?: string;
  isMapped: boolean;
  updates: number;
}

export interface FileEntryStatusDto extends EntityDto {
  countOfStatusFileEntry: number;
  fileEntryStatus?: string;
}

export interface GetFileEntryInput {
  server?: string;
  directory?: string;
  fileName?: string;
  listName: ListType;
}
