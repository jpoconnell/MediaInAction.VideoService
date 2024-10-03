using System;
using System.Threading.Tasks;
using MediaInAction.DelugeService.DelugeTorrentsNs;
using MediaInAction.DelugeService.TorrentsNs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Uow;

namespace MediaInAction.DelugeService;

public class TorrentAcknowledgeHandler : IDistributedEventHandler<DelugeTorrentAcceptedEto>, ITransientDependency
{
    private readonly IDelugeTorrentRepository _delugeTorrentRepository;

    public TorrentAcknowledgeHandler(IDelugeTorrentRepository delugeTorrentRepository)
    {
        _delugeTorrentRepository = delugeTorrentRepository;
    }
    
    [UnitOfWork]
    public virtual async Task HandleEventAsync(DelugeTorrentAcceptedEto eventData)
    {
      
    }
}