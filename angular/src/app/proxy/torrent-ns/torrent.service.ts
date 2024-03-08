import type { GetTorrentInput, GetTorrentsInput, TorrentCreatedDto, TorrentDto } from './dtos/models';
import { RestService, Rest } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class TorrentService {
  apiName = 'Default';
  

  create = (input: TorrentCreatedDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TorrentDto>({
      method: 'POST',
      url: '/api/app/torrent',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TorrentDto>({
      method: 'GET',
      url: `/api/app/torrent/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getTorrent = (input: GetTorrentInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TorrentDto>({
      method: 'GET',
      url: '/api/app/torrent/torrent',
      params: { alias: input.alias },
    },
    { apiName: this.apiName,...config });
  

  getTorrents = (getTorrentInput: GetTorrentsInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TorrentDto[]>({
      method: 'GET',
      url: '/api/app/torrent/torrents',
      params: { processed: getTorrentInput.processed },
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
