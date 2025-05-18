import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';
import type { MediaStatus } from '../../../shared/domain/enums/media-status.enum';
import type { EpisodeAliasDto } from '../../episode-alias-ns/models';

export interface ActiveDto extends EntityDto {
  seriesName?: string;
  pictureUrl?: string;
  seasons: number;
  episodes: number;
}

export interface DashboardDto extends EntityDto {
  actives: ActiveDto[];
  episodeStatusDto: EpisodeDto[];
}

export interface DashboardInput {
  filter?: string;
}

export interface EpisodeDto extends EntityDto<string> {
  seriesId?: string;
  seriesName?: string;
  seasonNum: number;
  episodeNum: number;
  episodeStatus: MediaStatus;
  airedDate?: string;
  episodeName?: string;
  altEpisodeId?: string;
  seasonEpisode?: string;
  episodeAliasDtos: EpisodeAliasDto[];
}

export interface GetEpisodeInput {
  seriesId?: string;
  seasonNum: number;
  episodeNum: number;
}

export interface GetEpisodesInput extends PagedAndSortedResultRequestDto {
  filter?: string;
}

export interface GetMyEpisodesInput {
  filter?: string;
}
