import type { MovieStatus } from './movie-status.enum';

export interface MovieAliasCreateDto {
  idType?: string;
  idValue?: string;
}

export interface MovieCreateDto {
  name?: string;
  firstAiredYear: number;
  imageName?: string;
  movieAliases: MovieAliasCreateDto[];
  movieStatus?: MovieStatus;
}
