using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.VideoService.Enums;
using MediaInAction.VideoService.Permissions;
using MediaInAction.VideoService.SeriesNs.Dtos;
using MediaInAction.VideoService.TorrentNs.Dtos;
using MediaInAction.VideoService.TorrentsNs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Specifications;

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

    async Task<PagedResultDto<TorrentDto>> ITorrentAppService.GetTorrentsAsync(GetTorrentsInput input)
    {
        ISpecification<Torrent> specification = Specifications.SpecificationFactory.Create("a:");

        var torrents =
            await _torrentRepository.GetListPagedAsync(specification, input.SkipCount,
                input.MaxResultCount, "" );

        var torrentDtoList = new  List<TorrentDto>();
        foreach (var torrent in torrents)
        {
            var torrentDto = MapToDto(torrent);
            torrentDtoList.Add(torrentDto);
        }
        var totalCount = await _torrentRepository.GetCountAsync();
        return new PagedResultDto<TorrentDto>(totalCount, torrentDtoList);
    }

    private IReadOnlyList<SeriesDto> CreateTorrentDtoMapping(object torrents)
    {
        throw new NotImplementedException();
    }

    public async Task<TorrentDto> CreateAsync(TorrentCreateDto input)
    {
        var torrent = await _torrentManager.CreateAsync(input);
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

    public Task<PagedResultDto<TorrentDto>> GetListPagedAsync(PagedAndSortedResultRequestDto pagedAndSortedResultRequestDto)
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
