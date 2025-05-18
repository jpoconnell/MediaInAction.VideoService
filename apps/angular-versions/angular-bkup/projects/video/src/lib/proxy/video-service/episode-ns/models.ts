import type { EpisodeAliasCreateDto } from '../episode-alias-ns/models';

export interface EpisodeCreateDto {
  seriesName?: string;
  seriesYear?: string;
  seasonNum: number;
  episodeNum: number;
  airedDate?: string;
  episodeName?: string;
  episodeCreateAliasesDto: EpisodeAliasCreateDto[];
}
