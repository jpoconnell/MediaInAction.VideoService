import type { AuditedEntityDto } from '@abp/ng.core';
import type { ListType } from '../../shared/domain/enums/list-type.enum';
import type { FileStatus } from '../../shared/domain/enums/file-status.enum';

export interface FileEntryDto extends AuditedEntityDto<string> {
  server?: string;
  directory?: string;
  filename?: string;
  extn?: string;
  size: number;
  sequence: number;
  listName: ListType;
  fileStatus: FileStatus;
}
