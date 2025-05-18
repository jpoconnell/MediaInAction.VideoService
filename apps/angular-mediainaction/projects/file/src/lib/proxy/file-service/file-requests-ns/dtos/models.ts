import type { CreationAuditedEntityDto } from '@abp/ng.core';
import type { FileOperation } from '../../../shared/domain/enums/file-operation.enum';

export interface FileRequestCompleteInputDto {
  token?: string;
  traktTypeId: number;
}

export interface FileRequestDto extends CreationAuditedEntityDto<string> {
  server: string;
  fileName?: string;
  directory: boolean;
  fileOperation?: FileOperation;
  moveToServer?: string;
  moveToFileName?: string;
  moveToDirectory: boolean;
}

export interface FileRequestStartDto {
  fileRequestId?: string;
  returnUrl: string;
  cancelUrl?: string;
}

export interface FileRequestStartResultDto {
  checkoutLink?: string;
}
