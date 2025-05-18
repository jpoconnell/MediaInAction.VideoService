import type { ListType } from '../../shared/domain/enums/list-type.enum';
import type { FileStatus } from '../../shared/domain/enums/file-status.enum';

export interface FileEntryCreateDto {
  fileEntryId?: string;
  server?: string;
  fileName?: string;
  directory?: string;
  extn?: string;
  size: number;
  sequence: number;
  listName: ListType;
  fileStatus: FileStatus;
}
