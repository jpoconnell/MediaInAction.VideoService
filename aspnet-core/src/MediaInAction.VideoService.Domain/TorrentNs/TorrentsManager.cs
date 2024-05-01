using System;
using System.Threading.Tasks;
using MediaInAction.VideoService.Enums;
using Microsoft.Extensions.Logging;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Guids;

namespace MediaInAction.VideoService.TorrentsNs;
public class TorrentManager : DomainService
{
    private readonly ITorrentRepository _torrentRepository;
    private readonly ILogger<TorrentManager> _logger;
    
    public TorrentManager(ITorrentRepository torrentRepository,
        ILogger<TorrentManager> logger,
        IDistributedEventBus distributedEventBus)
    {
        _torrentRepository = torrentRepository;
        _logger = logger;
    }

    public async Task<Torrent> CreateAsync(
        string comment,
        bool isSeed,
        string hash,
        bool paused ,
        double ratio ,
        string message, 
        string name,
        string label, 
        long added,
        double completeTime, 
        string location,
        FileStatus status,
        MediaType type,
        Guid mediaLink,
        Guid episodeLink,
        bool isMapped
    )
    {
        // Create new torrent
        var torrent = new Torrent(
            id: GuidGenerator.Create(),
            comment: comment,
            isSeed: isSeed,
            hash: hash,
            paused: paused,
            ratio: ratio,
            message: message,
            name: name,
            label: label,
            added: added,
            completeTime: completeTime,
            downloadLocation: location,
            status: status,
            type: type,
            mediaLink: Guid.Empty,
            episodeLink: Guid.Empty,
            isMapped: false
        );
        
        var dbTorrent = await _torrentRepository.FindByHash(torrent.Hash);

        if (dbTorrent == null)
        {
            var createdTorrent = await _torrentRepository.InsertAsync(torrent, true);
            return createdTorrent;
        }
        else
        {
            var update = 0;

            if (dbTorrent.Added != added)
            {
                dbTorrent.Added = added;
                update++;
            }

            if (dbTorrent.CompleteTime != completeTime)
            {
                dbTorrent.CompleteTime = completeTime;
                update++;
            }

            if (dbTorrent.Paused != paused)
            {
                dbTorrent.Paused = paused;
            }

            if (dbTorrent.IsSeed != isSeed)
            {
                dbTorrent.IsSeed = isSeed;
            }
            
            if (update > 0)
            {
                await _torrentRepository.UpdateAsync(dbTorrent);
            }
            return dbTorrent;
        }
    }
    
    public async Task<Torrent> AcceptTorrentAsync(string comment, bool isSeed, 
        string hash, bool paused, double ratio, string message, string name, 
        string label, long added, double complete, string location, 
        FileStatus status, MediaType mediaType, Guid mediaLink, Guid episodeLink)
    {
       var torrent = await CreateAsync(comment, isSeed,
            hash, paused, ratio, message, name,
            label, added, complete, location,
            status, mediaType, mediaLink, episodeLink, false);
       return torrent;
    }
}
