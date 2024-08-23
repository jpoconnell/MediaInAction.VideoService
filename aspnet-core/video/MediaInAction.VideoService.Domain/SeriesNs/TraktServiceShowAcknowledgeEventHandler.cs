using System;
using System.Threading.Tasks;
using MediaInAction.Shared.Domain.TraktService.TraktShowNs;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;

namespace MediaInAction.VideoService.SeriesNs;

public class TraktServiceShowAcknowledgeEventHandler : IDistributedEventHandler<TraktShowAcknowledgeEto>, ITransientDependency
{
    private readonly IDistributedEventBus _eventBus;
    private readonly ILogger<TraktServiceShowAcknowledgeEventHandler> _logger;
    private readonly SeriesManager _seriesManager;
    
    public TraktServiceShowAcknowledgeEventHandler(
        IDistributedEventBus eventBus,
        SeriesManager seriesManager,
        ILogger<TraktServiceShowAcknowledgeEventHandler> logger)
    {
        _eventBus = eventBus;
        _logger = logger;
        _seriesManager = seriesManager;
    }

    public async Task HandleEventAsync(TraktShowAcknowledgeEto eventData)
    {
        if (!Guid.TryParse(eventData.TraktId, out var traktId))
        {
            throw new BusinessException(VideoServiceErrorCodes.TraktShowIdNotGuid);
        }
        await _seriesManager.AcceptTraktSeriesAsync(eventData);
        _logger.LogInformation("Sending Trakt Show Accepted Event");
    }
}