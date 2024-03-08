import type { EntityDto } from '@abp/ng.core';
import type { EpisodeAliasCreateDto, EpisodeAliasDto } from '../../episode-alias-ns/models';
import type { MediaStatus } from '../../enums/media-status.enum';

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

export interface EpisodeCreateDto {
  seriesId: string;
  seasonNum: number;
  episodeNum: number;
  airedDate?: string;
  episodeStatus?: string;
  episodeStatusId: number;
  episodeName?: string;
  altEpisodeId?: string;
  seasonEpisode?: string;
  episodeAliases: EpisodeAliasCreateDto[];
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

export interface GetEpisodesInput {
  filter?: string;
}

export interface GetMyEpisodesInput {
  filter?: string;
}
