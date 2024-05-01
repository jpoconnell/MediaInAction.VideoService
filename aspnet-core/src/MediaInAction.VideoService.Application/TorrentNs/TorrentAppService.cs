using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.Enums;
using MediaInAction.VideoService.Permissions;
using MediaInAction.VideoService.TorrentNs.Dtos;
using MediaInAction.VideoService.TorrentsNs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace MediaInAction.VideoService.TorrentNs;

[Authorize(VideoServicePermissions.Torrents.Default)]
public class TorrentAppService : VideoServiceAppService, ITorrentAppService
{
    private readonly TorrentManager _torrentManager;
    private readonly ITorrentRepository _torrentRepository;
    private readonly ILogger<TorrentAppService> _logger;
    
    public TorrentAppService(TorrentManager movieManager,
        ITorrentRepository movieRepository,
        ILogger<TorrentAppService> logger
    )
    {
        _torrentManager = movieManager;
        _torrentRepository = movieRepository;
        _logger = logger;
    }
    
    public async Task<TorrentDto> GetAsync(Guid id)
    {
        var torrent = await _torrentRepository.GetAsync(id);
        return null;
    }

    public async Task<TorrentDto> CreateAsync(TorrentCreatedDto input)
    {
        var torrent = await _torrentManager.CreateAsync
        (
            comment: input.Comment,
            isSeed: input.IsSeed,
            hash: input.Hash,
            paused: input.Paused,
            ratio: input.Ratio,
            message: input.Message,
            name: input.Name,
            label: input.Label,
            added: input.Added,
            completeTime: input.CompleteTime,
            location: input.DownloadLocation,
            status: FileStatus.New,
            type: MediaType.Other,
            mediaLink: Guid.Empty,
            episodeLink: Guid.Empty,
            isMapped: false
        );

        return MapToDto(torrent);
    }

    private TorrentDto MapToDto(Torrent torrent)
    {
        var torrentDto = new TorrentDto
        {
            Hash = torrent.Hash,
            Name = torrent.Name,
            Comment = torrent.Comment,
            IsSeed = torrent.IsSeed,
            Paused = torrent.Paused,
            Ratio = torrent.Ratio,
            Message = torrent.Message,
            Label = torrent.Label,
            Added = torrent.Added,
            CompleteTime = torrent.CompleteTime,
            DownloadLocation = torrent.DownloadLocation,
            TorrentStatus = torrent.TorrentStatus,
            Type = torrent.Type,
            MediaLink = torrent.MediaLink,
            EpisodeLink = torrent.EpisodeLink,
            IsMapped = torrent.IsMapped
        };

        return torrentDto;
    }

    public async Task<List<TorrentDto>> GetTorrentsAsync(GetTorrentsInput getTorrentsInput)
    {
        if (getTorrentsInput.IsMapped != null)
        {
            var mapped = (bool) getTorrentsInput.IsMapped;
            var torrentList = await _torrentRepository.GetMapped( mapped);
            return ConvertToDto(torrentList);
        }
        else
        {
            var status = (FileStatus)getTorrentsInput.TorrentStatus;
            var torrentList = await _torrentRepository.GetTorrentStatus(status);
            return ConvertToDto(torrentList);
        }
    }

    public Task<TorrentDto> GetTorrentAsync(GetTorrentInput input)
    {
        throw new NotImplementedException();
    }

    private List<TorrentDto> ConvertToDto(List<Torrent> torrentList)
    {
        var torrentDtoList = new List<TorrentDto>();
        foreach (var torrent in torrentList)
        {
            var torrentDto = MapToDto(torrent);
            torrentDtoList.Add(torrentDto);
        }

        return torrentDtoList;
    }
}
