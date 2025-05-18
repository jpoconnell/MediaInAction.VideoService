
export interface EpisodeAliasCreateDto {
  idType?: string;
  idValue?: string;
}

export interface EpisodeCreateDto {
  slug?: string;
  seriesName?: string;
  seriesYear: number;
  seriesId?: string;
  seasonNum: number;
  episodeNum: number;
  airedDate?: string;
  episodeName?: string;
  episodeCreateAliases: EpisodeAliasCreateDto[];
}
