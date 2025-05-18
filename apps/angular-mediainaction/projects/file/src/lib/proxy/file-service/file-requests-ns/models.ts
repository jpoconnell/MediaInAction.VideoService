import type { FileOperation } from '../../shared/domain/enums/file-operation.enum';
import type { RequestStatus } from '../../shared/domain/enums/request-status.enum';

export interface FileRequestCreateDto {
  referenceId?: string;
  operation?: FileOperation;
  status?: RequestStatus;
  files: FileRequestFileCreateDto[];
}

export interface FileRequestFileCreateDto {
  server: string;
  fileName: string;
  directory: string;
  sequence: number;
}
