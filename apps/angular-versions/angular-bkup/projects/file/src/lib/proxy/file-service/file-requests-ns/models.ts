import type { FileOperation } from '../../shared/domain/enums/file-operation.enum';
import type { RequestStatus } from '../../shared/domain/enums/request-status.enum';
import type { EntityDto } from '@abp/ng.core';

export interface FileRequestCreateDto {
  referenceId?: string;
  operation: FileOperation;
  mediaEntryId?: string;
  mediaEntryType?: string;
  status: RequestStatus;
  files: FileRequestFileCreateDto[];
}

export interface FileRequestDto extends EntityDto<string> {
  name?: string;
  firstAiredYear: number;
  slug?: string;
  fileRequestFiles: FileRequestFileDto[];
}

export interface FileRequestFileCreateDto {
  server: string;
  fileName: string;
  directory: string;
  sequence: number;
}

export interface FileRequestFileDto extends EntityDto<string> {
  fileRequestId?: string;
  server?: string;
  fileName?: string;
  directory?: string;
}
