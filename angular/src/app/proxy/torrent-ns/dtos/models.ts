import type { EntityDto } from '@abp/ng.core';
import type { FileStatus } from '../../enums/file-status.enum';
import type { MediaType } from '../../enums/media-type.enum';

export interface GetTorrentInput {
  alias?: string;
}

export interface GetTorrentsInput {
  processed: boolean;
}

export interface TorrentCreatedDto {
  comment?: string;
  isSeed: boolean;
  hash?: string;
  paused: boolean;
  ratio: number;
  message?: string;
  name?: string;
  label?: string;
  added: number;
  completeTime: number;
  downloadLocation?: string;
}

export interface TorrentDto extends EntityDto<string> {
  comment?: string;
  isSeed: boolean;
  hash?: string;
  paused: boolean;
  ratio: number;
  message?: string;
  name?: string;
  label?: string;
  added: number;
  completeTime: number;
  downloadLocation?: string;
  torrentStatus: FileStatus;
  type: MediaType;
  mediaLink?: string;
  episodeLink?: string;
  updates: number;
  isMapped: boolean;
}
