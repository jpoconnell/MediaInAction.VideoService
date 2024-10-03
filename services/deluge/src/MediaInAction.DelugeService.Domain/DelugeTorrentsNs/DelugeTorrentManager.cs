using System;
using System.Threading.Tasks;
using MediaInAction.DelugeService.DelugeTorrentNs;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Distributed;

namespace MediaInAction.DelugeService.DelugeTorrentsNs
{
    public class DelugeTorrentManager : DomainService
    {
        private readonly IDelugeTorrentRepository _torrentRepository;
        private readonly IDistributedEventBus _distributedEventBus;
        private ILogger<DelugeTorrentManager> _logger;
        
        public DelugeTorrentManager(
            IDelugeTorrentRepository torrentRepository,
            IDistributedEventBus distributedEventBus,
            ILogger<DelugeTorrentManager> logger)
        {
            _distributedEventBus = distributedEventBus;
            _torrentRepository = torrentRepository;
            _logger = logger;
        }

        public async Task<DelugeTorrent> CreateAsync(DelugeTorrentCreateDto input)
        {
            try
            {
                Check.NotNullOrWhiteSpace(input.Name, nameof(input.Name));
                Check.NotNullOrWhiteSpace(input.Hash, nameof(input.Hash));

                var newTorrent = new DelugeTorrent(
                    GuidGenerator.Create(),
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
                    location: input.DownloadLocation
                    );
                var createdTorrent = await _torrentRepository.InsertAsync(newTorrent, true);
                 // TODO: Add event to notify other services
                 return createdTorrent;
            }
            catch (Exception ex)
            {
                _logger.LogDebug("CreateAsync:" + ex.Message);
                return null;
            }
        }
    }
}
