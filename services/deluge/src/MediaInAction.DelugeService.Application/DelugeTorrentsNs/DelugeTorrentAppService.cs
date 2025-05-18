using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediaInAction.DelugeService.DelugeTorrentNs;
using MediaInAction.DelugeService.DelugeTorrentNs.Dtos;
using MediaInAction.DelugeService.DelugeTorrentsNs.Specifications;
using MediaInAction.DelugeService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Specifications;

namespace MediaInAction.DelugeService.DelugeTorrentsNs;

[Authorize(DelugeServicePermissions.Torrents.Default)]
public class DelugeTorrentAppService : DelugeServiceAppService, IDelugeTorrentAppService
{
    private readonly ILogger<DelugeTorrentAppService> _logger;
    private readonly IDelugeTorrentRepository _delugeTorrentRepository;
    private readonly DelugeTorrentManager _delugeMovieManager;

    public DelugeTorrentAppService(
        IDelugeTorrentRepository movieRepository,
        ILogger<DelugeTorrentAppService> logger,
        DelugeTorrentManager delugeMovieManager)
    {
        _delugeTorrentRepository = movieRepository;
        _delugeMovieManager = delugeMovieManager;
        _logger = logger;
    }

    public async Task<DelugeTorrentDto> GetAsync(Guid id)
    {
        var delugeMovie = await _delugeTorrentRepository.GetAsync(id);
        return ObjectMapper.Map<DelugeTorrent, DelugeTorrentDto>(delugeMovie);
    }

    public Task<PagedResultDto<DelugeTorrentNs.Dtos.DelugeTorrentDto>> GetTorrentListPagedAsync(GetTorrentListDto filter)
    {
        throw new NotImplementedException();
    }
}